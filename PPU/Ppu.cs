using YaNes.PPU.Mirroring;
using YaNes.PPU.Registers;
using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNes.PPU
{
    public class Ppu : IPpu
    {
        private const byte OamSpriteSizeBytes = 4;

        private readonly byte[] ram = new byte[2048];
        private readonly byte[] paletteTable = new byte[32];
        private readonly byte[] oamData = new byte[64 * OamSpriteSizeBytes];
        private readonly byte[] secondaryOamData = new byte[64 * OamSpriteSizeBytes]; // TODO : should be 8 * OamSpriteSizeBytes

        private MirroringMode mirroringMode = new UnknownMirroringMode();
        private IRom? rom;
        private IInterruptsListener? interruptsListener;

        private readonly Controller controller = new(0b1000_0000);
        private byte mask = 0;
        private readonly Status status = new();
        private byte oamAddress = 0;
        private byte scroll = 0;
        private readonly Address address = new();
        private byte dataBuffer;

        public int Scanline { get; private set; } = 0;
        public int ScanlineCycle { get; private set; } = 0;

        public byte Controller { get => controller.State; set => controller.State = value; }
        public byte Mask { get => throw new NotImplementedException(); set => mask = value; }
        public byte Status { get => status.State; set => status.State = value; }
        public byte OamAddress { get => throw new NotImplementedException(); set => oamAddress = value; }
        public byte OamData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Scroll { get => throw new NotImplementedException(); set => scroll = value; }
        public byte Address { get => address.State; set => address.State = value; }
        public byte Data
        {
            get
            {
                var result = dataBuffer;
                var dataAddress = address.Buffer;
                address.Increment(controller.VramIncrement);

                if (dataAddress.InRange(ReservedAddresses.ChrRomAddressSpace))
                {
                    dataBuffer = rom!.Read8bitChr(dataAddress);
                }
                else if (dataAddress.InRange(ReservedAddresses.RamAddressSpace))
                {
                    dataBuffer = ram[mirroringMode.MirrorVramAddress(dataAddress)];
                }
                else if (dataAddress.InRange(ReservedAddresses.ForbiddenAddressSpace))
                {
                    throw new InvalidOperationException();
                }
                else if (dataAddress.InRange(ReservedAddresses.PaletteAddressSpace))
                {
                    dataBuffer = paletteTable[MirrorPaletteTableAddress(dataAddress)];
                }
                else
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
            set
            {
                var dataAddress = address.Buffer;
                address.Increment(controller.VramIncrement);

                if (dataAddress.InRange(ReservedAddresses.RamAddressSpace))
                {
                    var ramAddress = mirroringMode.MirrorVramAddress(dataAddress);
                    ram[ramAddress] = value;
                }
                else if (dataAddress.InRange(ReservedAddresses.PaletteAddressSpace))
                {
                    var paletteTableAddress = MirrorPaletteTableAddress(dataAddress);
                    paletteTable[paletteTableAddress] = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private static ushort MirrorPaletteTableAddress(ushort address)
        {
            return (ushort)(address - 0x3F00);
        }

        public void AttachRom(IRom rom)
        {
            this.rom = rom;

            if (rom.Mirroring == 0)
                mirroringMode = new HorizontalMirroringMode();
            else
                throw new NotImplementedException();
        }

        public void AttachInterruptsListener(IInterruptsListener interruptsListener)
        {
            this.interruptsListener = interruptsListener;
        }

        public void Update(int cycles)
        {
            ScanlineCycle += cycles;

            while (ScanlineCycle >= 341)
            {
                ScanlineCycle -= 341;
                Scanline++;

                if (Scanline < 241)
                {
                    FetchCurrentScanlineSprites();
                }
                else if (Scanline == 241)
                {
                    status.Set(Registers.Status.Flags.VerticalBlank, true);

                    if (controller.Get(Registers.Controller.Flags.GenerateNmi))
                        interruptsListener?.Trigger(Interrupt.NMI);
                }
                else if (Scanline == 262)
                {
                    Scanline = 0;
                    status.Set(Registers.Status.Flags.VerticalBlank, false);
                }
            }
        }

        // TODO : it can be performed one time per each WriteOamData call but it will take 256 * 8 * 4 bytes to store the result (it works for me)
        private void FetchCurrentScanlineSprites()
        {
            var secondaryDataPointer = 0;

            for (var primaryDataPointer = 0; primaryDataPointer < oamData.Length; primaryDataPointer += OamSpriteSizeBytes)
            {
                var spriteTopY = oamData[primaryDataPointer];

                // TODO : fix secondaryOamData length
                // if (spriteTopY == 0)
                //    continue;

                if (Scanline < spriteTopY)
                    continue;

                var spriteBottomY = spriteTopY + 7;

                if (Scanline > spriteBottomY)
                    continue;

                secondaryOamData[secondaryDataPointer++] = oamData[primaryDataPointer];
                secondaryOamData[secondaryDataPointer++] = oamData[primaryDataPointer + 1];
                secondaryOamData[secondaryDataPointer++] = oamData[primaryDataPointer + 2];
                secondaryOamData[secondaryDataPointer++] = oamData[primaryDataPointer + 3];
            }

            if (secondaryDataPointer < secondaryOamData.Length)
                secondaryOamData[secondaryDataPointer] = 255;
        }

        public byte[] GetPixelColor(int x, int y)
        {
            var bgPixelColor = GetBgPixelColor(x, y);
            var spritePixelColor = GetSpritePixelColor(x, y, bgPixelColor);
            return spritePixelColor;
        }

        private byte[] GetBgPixelColor(int x, int y)
        {
            var nametableIndex = Controller & 0b0000_0011; // TODO : add method to Controller class
            var baseNametableAddress = nametableIndex switch
            {
                0 => 0x0000,
                1 => 0x0400,
                2 => 0x0000,
                3 => 0x0400,
                _ => throw new ArgumentOutOfRangeException()
            };
            const byte tileSizePixels = 8;
            var nametableX = x / tileSizePixels;
            var nametableY = y / tileSizePixels;
            const byte nametableWidthTiles = 32;
            var nametableAddress = baseNametableAddress + nametableX + nametableY * nametableWidthTiles;
            var tile = ram[nametableAddress];
            const byte tileSizeBytes = 16;
            var bgTilesBaseAddress = (Controller & 0b0001_0000) == 0 ? 0x0000 : 0x1000; // TODO : add method to Controller class
            var tileStart = bgTilesBaseAddress + tile * tileSizeBytes;
            var tileX = x % tileSizePixels;
            var tileY = y % tileSizePixels;
            var firstByte = rom!.Read8bitChr((ushort)(tileStart + tileY));
            firstByte = (byte)(firstByte << tileX);
            var secondByte = rom!.Read8bitChr((ushort)(tileStart + tileY + 8));
            secondByte = (byte)(secondByte << tileX);
            var colorCode = ((firstByte & 0b1000_0000) >> 7) + ((secondByte & 0b1000_0000) >> 6);

            if (colorCode == 0)
                return Palette.GetColor(paletteTable[0]);

            const int attributeTableOffset = 0x3C0;
            var metaTileX = nametableX / 4;
            var metaTileY = nametableY / 4;
            var attributeTableIndex = metaTileX + metaTileY * 8;
            var metaTileAttributeAddress = baseNametableAddress + attributeTableOffset + attributeTableIndex;
            var metaTileAttribute = ram[metaTileAttributeAddress];
            var metaTileInnerX = (nametableX % 4) / 2;
            var metaTileInnerY = (nametableY % 4) / 2;
            var shift = (metaTileInnerX * 2) + (metaTileInnerY * 4);
            var paletteIndex = (metaTileAttribute >> shift) & 0b11;

            return Palette.GetColor(paletteTable[colorCode + paletteIndex * 4]);
        }

        private byte[] GetSpritePixelColor(int x, int y, byte[] bgPixelColor)
        {
            var result = bgPixelColor;

            for (var spriteStart = 0; spriteStart < secondaryOamData.Length; spriteStart += OamSpriteSizeBytes)
            {
                var spriteTopY = secondaryOamData[spriteStart];

                if (spriteTopY == 255)
                    return result;

                var spriteLeftX = secondaryOamData[spriteStart + 3];

                if (x < spriteLeftX)
                    continue;

                var spriteRightX = spriteLeftX + 7;

                if (x > spriteRightX)
                    continue;

                var attributes = secondaryOamData[spriteStart + 2];

                // TODO : check if bg color is zero
                // https://www.nesdev.org/wiki/PPU_rendering
                var isBackgroundPriority = (attributes & 0b0010_0000) > 1;

                if (isBackgroundPriority)
                    return result;

                var tileIndex = secondaryOamData[spriteStart + 1];
                var spritePatternTableIndex = Controller & 0b0000_1000; // TODO : add method to Controller class
                var spritePatternBaseAddress = spritePatternTableIndex == 0 ? 0x0000 : 0x1000;

                var tileSizeBytes = 16;
                var tileStart = spritePatternBaseAddress + tileIndex * tileSizeBytes;

                var spriteSpaceX = x - spriteLeftX;
                var flipHorizontally = (attributes & 0b0100_0000) > 1;
                spriteSpaceX = flipHorizontally ? 7 - spriteSpaceX : spriteSpaceX;

                var spriteSpaceY = y - secondaryOamData[spriteStart];
                var flipVertically = (attributes & 0b1000_0000) > 1;
                spriteSpaceY = flipVertically ? 7 - spriteSpaceY : spriteSpaceY;

                // TODO : copy paste
                var firstByte = rom!.Read8bitChr((ushort)(tileStart + spriteSpaceY));
                firstByte = (byte)(firstByte << spriteSpaceX);
                var secondByte = rom!.Read8bitChr((ushort)(tileStart + spriteSpaceY + 8));
                secondByte = (byte)(secondByte << spriteSpaceX);
                var colorCode = ((firstByte & 0b1000_0000) >> 7) + ((secondByte & 0b1000_0000) >> 6);

                var transparentPixel = colorCode == 0;

                if (transparentPixel)
                    continue;

                var paletteIndex = attributes & 0b0000_0011;
                var color = paletteTable[(4 + paletteIndex) * 4 + colorCode]; // min sprite palette is 4
                result = Palette.GetColor(color);
            }

            return result;
        }

        public void WriteOamData(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
                oamData[oamAddress++] = buffer[i];
        }
    }
}

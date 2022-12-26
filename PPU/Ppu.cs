using YaNes.PPU.Mirroring;
using YaNes.PPU.Preprocessing;
using YaNes.PPU.Registers;
using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNes.PPU
{
    public class Ppu : IPpu
    {
        private readonly byte[] ram = new byte[2048];
        private readonly byte[] paletteTable = new byte[32];
        private readonly byte[] oamData = new byte[64 * Constants.Ppu.OamSpriteSizeBytes];
        private readonly byte[] secondaryOamData = new byte[64 * Constants.Ppu.OamSpriteSizeBytes];

        private readonly PixelsPreprocessor preprocessor = new PixelsPreprocessor();

        private MirroringMode mirroringMode = new UnknownMirroringMode();
        private IRom? rom;
        private IInterruptsListener? interruptsListener;

        private readonly Controller controller = new(0b1000_0000);
        private readonly Status status = new();
        private readonly Address address = new();

        private byte dataBuffer;

        public int Scanline { get; private set; } = 0;
        public int ScanlineCycle { get; private set; } = 0;

        public byte Controller
        {
            private get => controller.State;
            set
            {
                controller.State = value;
                OpenBus = value;
            }
        }

        public byte Mask { private get; set; }
        public byte Status { get => status.State; private set => status.State = value; }
        public byte OamAddress { private get; set; }
        public byte OamData { get => throw new NotImplementedException(); }
        public byte Scroll { private get; set; }
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
        public byte OpenBus { get; private set; }

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

        private void FetchCurrentScanlineSprites()
        {
            var secondaryDataPointer = 0;

            for (var primaryDataPointer = 0; primaryDataPointer < oamData.Length; primaryDataPointer += Constants.Ppu.OamSpriteSizeBytes)
            {
                var spriteTopY = oamData[primaryDataPointer];

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
            var bgPixelColor = GetBgPixelColor(preprocessor.GetBgPixel(x, y));
            var spritePixelColor = GetSpritePixelColor(x, y, bgPixelColor);
            return spritePixelColor;
        }

        private byte[] GetBgPixelColor(PrecomputedBgPixelValues precomputed)
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

            var nametableAddress = baseNametableAddress + precomputed.NametableAddressShift;
            var tile = ram[nametableAddress];
            var bgTilesBaseAddress = (Controller & 0b0001_0000) == 0 ? 0x0000 : 0x1000; // TODO : add method to Controller class
            var tileStart = bgTilesBaseAddress + tile * Constants.Ppu.TileSizeBytes;

            var firstByte = rom!.Read8bitChr((ushort)(tileStart + precomputed.TileSpaceY));
            firstByte = (byte)(firstByte << precomputed.TileSpaceX);
            var secondByte = rom!.Read8bitChr((ushort)(tileStart + precomputed.TileSpaceY + 8));
            secondByte = (byte)(secondByte << precomputed.TileSpaceX);
            var colorCode = ((firstByte & 0b1000_0000) >> 7) + ((secondByte & 0b1000_0000) >> 6);

            if (colorCode == 0)
                return Palette.GetColor(paletteTable[0]);

            var metaTileAttributeAddress = baseNametableAddress + Constants.Ppu.AttributeTableOffset + precomputed.AttributeTableIndex;
            var metaTileAttribute = ram[metaTileAttributeAddress];
            var paletteIndex = (metaTileAttribute >> precomputed.AttributeShift) & 0b11;

            return Palette.GetColor(paletteTable[colorCode + paletteIndex * 4]);
        }

        private byte[] GetSpritePixelColor(int x, int y, byte[] bgPixelColor)
        {
            var result = bgPixelColor;

            for (var spriteStart = 0; spriteStart < secondaryOamData.Length; spriteStart += Constants.Ppu.OamSpriteSizeBytes)
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
                oamData[OamAddress++] = buffer[i];
        }
    }
}

using YaNes.PPU.Preprocessing;
using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNes.PPU
{
    public partial class Ppu : IPpu, IPpuRegisters
    {
        private readonly PixelsPreprocessor preprocessor = new();
        private readonly byte[] oamData = new byte[64 * Constants.Ppu.OamSpriteSizeBytes];
        private readonly byte[] secondaryOamData = new byte[64 * Constants.Ppu.OamSpriteSizeBytes];

        public void WriteOamData(byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
                oamData[OamAddress++] = buffer[i];
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
    }
}

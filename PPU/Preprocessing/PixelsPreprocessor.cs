
using YaNES.Core.Utils;

namespace YaNES.PPU.Preprocessing
{
    internal class PixelsPreprocessor
    {
        private readonly PrecomputedBgPixelValues[] bgData = new PrecomputedBgPixelValues[Constants.Nes.ScreenWidth * Constants.Nes.ScreenHeight];

        public PixelsPreprocessor()
        {
            for (var x = 0; x < Constants.Nes.ScreenWidth; x++)
            {
                for (var y = 0; y < Constants.Nes.ScreenHeight; y++)
                {
                    bgData[y * Constants.Nes.ScreenWidth + x] = ComputeBgPixelValues(x, y);
                }
            }
        }

        private static PrecomputedBgPixelValues ComputeBgPixelValues(int x, int y)
        {
            var nametableX = x / Constants.Ppu.TileDimensionPixels;
            var nametableY = y / Constants.Ppu.TileDimensionPixels;
            var nametableAddressShift = nametableX + nametableY * Constants.Ppu.NametableWidthTiles;

            if (nametableAddressShift > ushort.MaxValue)
                throw new InvalidOperationException();

            var tileSpaceX = x % Constants.Ppu.TileDimensionPixels;
            var tileSpaceY = y % Constants.Ppu.TileDimensionPixels;

            if (tileSpaceX > byte.MaxValue || tileSpaceY > byte.MaxValue)
                throw new InvalidOperationException();

            var metaTileX = nametableX / 4;
            var metaTileY = nametableY / 4;
            var attributeTableIndex = metaTileX + metaTileY * 8;

            if (attributeTableIndex > byte.MaxValue)
                throw new InvalidOperationException();

            var metaTileInnerX = (nametableX % 4) / 2;
            var metaTileInnerY = (nametableY % 4) / 2;
            var shift = (metaTileInnerX * 2) + (metaTileInnerY * 4);

            if (shift > byte.MaxValue)
                throw new InvalidOperationException();

            return new PrecomputedBgPixelValues
            {
                NametableAddressShift = (ushort)nametableAddressShift,
                TileSpaceX = (byte)tileSpaceX,
                TileSpaceY = (byte)tileSpaceY,
                AttributeTableIndex = (byte)attributeTableIndex,
                AttributeShift = (byte)shift
            };
        }

        public PrecomputedBgPixelValues GetBgPixelValues(int x, int y)
        {
            return bgData[y * Constants.Nes.ScreenWidth + x];
        }
    }
}

namespace YaNES.Core.Utils
{
    public static class Constants
    {
        public static class Nes
        {
            public const int ScreenWidth = 256;
            public const int ScreenHeight = 240;
        }

        public static class Ppu
        {
            public const int CyclesPerCpuCycle = 3;

            public const int Scanlines = 262;
            public const int ScanlineCyclesDuration = 341;
            public const int CyclesPerFrame = Scanlines * ScanlineCyclesDuration;

            public const int TileSizeBytes = 16;
            public const int TileDimensionPixels = 8;
            public const int NametableWidthTiles = 32;

            public const int AttributeTableOffset = 0x3C0;

            public const int OamSpriteSizeBytes = 4;
        }
    }
}

using YaNES.Core;

namespace YaNes.PPU.Registers
{
    internal class Controller : Register8BitWith<Controller.Flags>
    {
        public enum Flags : byte
        {
            GenerateNmi = 1 << 7,
            MasterSlaveSelect = 1 << 6,
            SpriteSize = 1 << 5,
            BackgroundPatternAddress = 1 << 4,
            SpritePatternAddress = 1 << 3,
            VramAddIncrement = 1 << 2,
            Nametable2 = 1 << 1,
            Nametable1 = 1 << 0
        }

        public int VramIncrement => Get(Flags.VramAddIncrement) ? 32 : 1;

        public Controller(byte initialState)
        {
            State = initialState;
        }
    }
}

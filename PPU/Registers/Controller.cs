using YaNES.Core;

namespace PPU.Registers
{
    // TODO : create RegisterWithFlags<T> class to reduce code duplication
    internal class Controller : Register8Bit
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

        public bool Get(Flags flag) => (State & (byte)flag) > 0;

        public void Set(Flags flag, bool value)
        {
            if (value)
                State |= (byte)flag;
            else
                State &= (byte)~flag;
        }

        public int VramIncrement => Get(Flags.VramAddIncrement) ? 32 : 1;
    }
}

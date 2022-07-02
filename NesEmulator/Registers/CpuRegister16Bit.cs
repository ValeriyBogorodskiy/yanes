namespace NesEmulator.Registers
{
    internal abstract class CpuRegister16Bit
    {
        private ushort state;

        protected ushort State { get { return state; } set { state = value; } }
    }
}

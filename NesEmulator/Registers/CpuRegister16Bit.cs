namespace NesEmulator.Registers
{
    internal abstract class CpuRegister16Bit : IReadOnlyCpuRegister16Bit
    {
        private ushort state;
        public ushort State { get { return state; } protected set { state = value; } }
    }
}

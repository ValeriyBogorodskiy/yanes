namespace NesEmulator.Registers
{
    internal abstract class CpuRegister8Bit : IReadOnlyCpuRegister8Bit
    {
        private byte state;
        public byte State { get { return state; } protected set { state = value; } }
    }
}

namespace NesEmulator.Registers
{
    internal abstract class CpuRegister8Bit
    {
        private byte state;
        protected byte State { get { return state; } set { state = value; } }
    }
}

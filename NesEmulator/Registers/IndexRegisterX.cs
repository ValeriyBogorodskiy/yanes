namespace NesEmulator.Registers
{
    internal class IndexRegisterX : CpuRegister8Bit
    {
        internal void Load(byte value)
        {
            State = value;
        }
    }
}

namespace NesEmulator.Registers
{
    internal class Accumulator : CpuRegister8Bit
    {
        internal void Load(byte value)
        {
            State = value;
        }
    }
}

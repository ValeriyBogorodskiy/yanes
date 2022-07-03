namespace NesEmulator.Registers
{
    internal class ProgramCounter : CpuRegister16Bit
    {
        internal void Increment()
        {
            State++;
        }

        internal ushort Fetch() => State;
    }
}

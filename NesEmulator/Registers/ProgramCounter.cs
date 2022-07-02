namespace NesEmulator.Registers
{
    internal class ProgramCounter : CpuRegister16Bit
    {
        public void Increment()
        {
            State++;
        }

        public ushort Fetch() => State;
    }
}

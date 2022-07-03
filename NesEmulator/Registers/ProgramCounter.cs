namespace NesEmulator.Registers
{
    internal class ProgramCounter : IReadOnlyCpuRegister16Bit
    {
        public ushort State { get; private set; }

        internal void Increment()
        {
            State++;
        }

        internal ushort Fetch() => State;
    }
}

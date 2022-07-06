namespace NesEmulator.Registers
{
    internal class ProgramCounter
    {
        public ushort State { get; private set; }

        internal void SetState(ushort value) => State = value;

        internal void Increment()
        {
            State++;
        }
        internal void Reset() => State = 0;
    }
}

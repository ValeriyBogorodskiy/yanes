namespace NesEmulator.Registers
{
    internal abstract class CpuRegister8Bit
    {
        public byte State { get; protected set; }
        internal void Reset() => State = 0;
    }
}

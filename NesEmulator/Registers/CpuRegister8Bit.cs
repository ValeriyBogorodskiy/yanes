namespace NesEmulator.Registers
{
    internal abstract class CpuRegister8Bit : IReadOnlyCpuRegister8Bit
    {
        public byte State { get; protected set; }
    }
}

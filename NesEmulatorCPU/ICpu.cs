namespace NesEmulatorCPU
{
    public interface ICpu
    {
        IRAM RAM { get; }
        IRegisters Registers { get; }
        IEnumerable<bool> Run(byte[] program);
    }
}

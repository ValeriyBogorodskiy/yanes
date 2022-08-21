namespace NesEmulatorCPU
{
    public interface ICpu
    {
        IRAM RAM { get; }
        IRegisters Registers { get; }
        IEnumerable<InstructionExecutionResult> Run(byte[] program);
    }
}

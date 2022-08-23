namespace NesEmulatorCPU
{
    public interface ICpu
    {
        IRAM RAM { get; }
        IRegisters Registers { get; }
        IEnumerator<InstructionExecutionResult> Run(byte[] program);
    }
}

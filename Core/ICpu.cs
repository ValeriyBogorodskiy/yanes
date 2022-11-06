namespace YaNES.Core
{
    public interface ICpu
    {
        ICpuBus Bus { get; }
        ICpuRegisters Registers { get; }
        IInterruptsListener InterruptsListener { get; }
        CpuInstructionExecutionReport InsertCartridge(IRom rom);
        CpuInstructionExecutionReport ExecuteNextInstruction();
    }
}

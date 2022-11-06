namespace YaNES.Core
{
    public interface ICpu
    {
        ICpuBus Bus { get; }
        ICpuRegisters Registers { get; }
        IInterruptsSource InterruptsSource { get; }
        CpuInstructionExecutionReport InsertCartridge(IRom rom);
        CpuInstructionExecutionReport ExecuteNextInstruction();
    }
}

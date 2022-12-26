namespace YaNES.Core
{
    public interface ICpu
    {
        ICpuBus Bus { get; }
        ICpuRegisters Registers { get; }
        IInterruptsListener InterruptsListener { get; }
        IOamDmaTransferListener OamDmaTransferListener { get; }
        CpuInstructionExecutionReport InsertCartridge(IRom rom);
        CpuInstructionExecutionReport ExecuteNextInstruction();
    }
}

﻿namespace YaNES.Interfaces
{
    public interface ICpu
    {
        ICpuBus Bus { get; }
        ICpuRegisters Registers { get; }
        IEnumerator<CpuInstructionExecutionReport> Run(IRom rom);
    }
}

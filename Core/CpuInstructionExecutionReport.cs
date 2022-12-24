namespace YaNES.Core
{
    public struct CpuInstructionExecutionReport
    {
        public int Cycles;
        public CpuInstructionExecutionResult Result;
        public byte Opcode;

        public CpuInstructionExecutionReport(CpuInstructionExecutionResult result, byte opcode, int cycles)
        {
            Result = result;
            Opcode = opcode;
            Cycles = cycles;
        }
    }
}

namespace YaNES.Interfaces
{
    public class CpuInstructionExecutionReport
    {
        public CpuInstructionExecutionResult Result { get; }
        public ushort Opcode { get; }
        public int Cycles { get; }

        public CpuInstructionExecutionReport(CpuInstructionExecutionResult result, ushort opcode, int cycles)
        {
            Result = result;
            Opcode = opcode;
            Cycles = cycles;
        }
    }
}

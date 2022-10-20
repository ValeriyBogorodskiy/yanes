namespace YaNES.CPU
{
    // TODO : nested enum looks ugly and naming sucks
    public struct InstructionExecutionResult
    {
        public enum ResultCode
        {
            BeforeFirstInstruction,
            Success,
            Failure,
            ReachedEndOfProgram
        }

        public ResultCode Code;
        public int Cycles;
    }
}

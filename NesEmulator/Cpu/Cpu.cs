namespace NesEmulator.Cpu
{
    internal class Cpu
    {
        private CpuRegister16 programCounter = new();
        private CpuRegister8 stackPointer = new();
        private CpuRegister8 accumulator = new();
        private CpuRegister8 indexRegisterX = new();
        private CpuRegister8 indexRegisterY = new();
        private CpuRegister8 processorStatus = new();

        public void Run(Opcode[] program)
        {
            while (true)
            {
                var opcode = program[programCounter.State];
                programCounter.State++;
                var instruction = DecodeOpcode(opcode.Value);
                instruction.Execute();
            }
        }

        private Instruction DecodeOpcode(byte opcode)
        {
            return null;
        }
    }
}

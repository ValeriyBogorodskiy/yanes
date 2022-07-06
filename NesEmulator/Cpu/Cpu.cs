using NesEmulator.Tools;

namespace NesEmulator.Cpu
{
    // TODO: i have ICpu to hide implementation but how can i create Cpu instance without making class public (abstract factory?) 
    public class Cpu : ICpu
    {
        public ushort ProgramCounter => registers.ProgramCounter.State;
        public byte StackPointer => registers.StackPointer.State;
        public byte Accumulator => registers.Accumulator.State;
        public byte IndexRegisterX => registers.IndexRegisterX.State;
        public byte IndexRegisterY => registers.IndexRegisterY.State;
        public byte ProcessorStatus => registers.ProcessorStatus.State;

        private readonly Registers registers = new();
        private readonly RAM ram = new();
        private readonly Instructions instructions;

        internal Cpu()
        {
            instructions = new Instructions(registers, ram);
        }

        public void Run(byte[] program)
        {
            registers.Reset();
            ram.LoadProgram(program);
            var programStartAddress = ram.Read16bit(ReservedAddresses.ProgramStartPointerAddress);
            registers.ProgramCounter.SetState(programStartAddress);

            while (true)
            {
                var nextInstructionAddress = registers.ProgramCounter.State;
                var opcode = ram.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.Increment();

                if (opcode == 0)
                    break;

                instructions.GetInstructionForOpcode(opcode).Execute();
            }
        }
    }
}

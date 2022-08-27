using NesEmulatorCPU.Utils;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Instructions;

namespace NesEmulatorCPU
{
    public class Cpu
    {
        public IBus Bus => bus;
        public IRegisters Registers => registers;

        private CPUSettings settings;
        private readonly Bus bus = new();
        private readonly RegistersProvider registers = new();
        private readonly InstructionsProvider instructions = new();

        public Cpu(CPUSettings settings)
        {
            this.settings = settings;
        }

        // TODO : IEnumerator is the easiest way to achieve desired behaviour. I'll think about this later
        public IEnumerator<InstructionExecutionResult> Run(byte[] program)
        {
            LoadProgramToRam(program);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            while (true)
            {
                var nextInstructionAddress = registers.ProgramCounter.State;
                var opcode = bus.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.State++;

                if (opcode == 0)
                    break;

                var instruction = instructions.GetInstructionForOpcode(opcode);
                instruction.Execute(bus, registers);

                yield return InstructionExecutionResult.Success;
            }

            yield return InstructionExecutionResult.ReachedEndOfProgram;
        }

        private void LoadProgramToRam(byte[] program)
        {
            for (ushort i = 0; i < program.Length; i++)
            {
                var programByte = program[i];
                var programByteAddress = (ushort)(settings.StartingProgramAddress + i);

                bus.Write8Bit(programByteAddress, programByte);
            }

            bus.Write16Bit(ReservedAddresses.ProgramStartPointerAddress, settings.StartingProgramAddress);
        }

        private void SetupRegisters() => registers.Reset();

        private void SetupProgramCounter()
        {
            registers.ProgramCounter.State = bus.Read16bit(ReservedAddresses.ProgramStartPointerAddress);
        }

        private void SetupStackPointer()
        {
            registers.StackPointer.State = ReservedAddresses.StartingStackPointerAddress;
        }
    }
}

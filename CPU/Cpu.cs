using YaNES.CPU.Cartridge;
using YaNES.CPU.Instructions;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU
{
    public class CPU
    {
        public ICpuBus Bus => bus;
        public ICpuRegisters Registers => registers;

        private readonly CpuSettings settings;
        private readonly Bus bus = new();
        private readonly RegistersProvider registers = new();
        private readonly InstructionsProvider instructions = new();

        public CPU(CpuSettings settings)
        {
            this.settings = settings;
        }

        // TODO : IEnumerator is the easiest way to achieve desired behaviour. I'll think about this later
        public IEnumerator<InstructionExecutionResult> Run(ROM rom)
        {
            bus.InsertRom(rom);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            // TODO : not cool
            yield return new InstructionExecutionResult { Code = InstructionExecutionResult.ResultCode.BeforeFirstInstruction };

            while (true)
            {
                var nextInstructionAddress = registers.ProgramCounter.State;
                var opcode = bus.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.State++;

                if (opcode == 0)
                    break;

                var instruction = instructions.GetInstructionForOpcode(opcode);
                var cycles = instruction.Execute(bus, registers);

                yield return new InstructionExecutionResult() { Code = InstructionExecutionResult.ResultCode.Success, Cycles = cycles };
            }

            yield return new InstructionExecutionResult() { Code = InstructionExecutionResult.ResultCode.ReachedEndOfProgram };
        }

        private void SetupRegisters()
        {
            registers.Reset();
            registers.ProcessorStatus.State = settings.InitialProcessorStatus;
        }

        private void SetupProgramCounter()
        {
            registers.ProgramCounter.State = settings.StartingProgramAddress;
        }

        private void SetupStackPointer()
        {
            registers.StackPointer.State = ReservedAddresses.StartingStackPointerAddress;
        }
    }
}

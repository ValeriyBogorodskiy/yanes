using YaNES.Core;
using YaNES.CPU.Instructions;
using YaNES.CPU.Registers;

namespace YaNES.CPU
{
    public class Cpu : ICpu
    {
        // values are chosen to match first line of nestest.log
        private static readonly CpuInstructionExecutionReport ProcessInitialized = new(CpuInstructionExecutionResult.Success, 0, 7);

        public ICpuBus Bus => bus;
        public ICpuRegisters Registers => registers;

        private readonly CpuSettings settings;
        private readonly Bus bus = new();
        private readonly RegistersProvider registers = new();
        private readonly InstructionsProvider instructions = new();

        public Cpu(CpuSettings settings)
        {
            this.settings = settings;
        }

        // TODO : replace Run with 2 methods: Insert(IRom rom) & ExecuteNextInstruction():CpuInstructionExecutionReport
        public IEnumerator<CpuInstructionExecutionReport> Run(IRom rom)
        {
            bus.AttachRom(rom);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            yield return ProcessInitialized;

            while (true)
            {

                var nextInstructionAddress = registers.ProgramCounter.State;
                var opcode = bus.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.State++;

                if (opcode == 0)
                    break;

                var instruction = instructions.GetInstructionForOpcode(opcode);
                var cycles = instruction.Execute(bus, registers);
                yield return new CpuInstructionExecutionReport(CpuInstructionExecutionResult.Success, opcode, cycles);
            }
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

using YaNES.CPU.Instructions;
using YaNES.CPU.Registers;
using YaNES.Interfaces;

namespace YaNES.CPU
{
    public class CPU : ICpu
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

        public IEnumerator<CpuInstructionExecutionReport> Run(IRom rom)
        {
            bus.InsertRom(rom);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            yield return new CpuInstructionExecutionReport(CpuInstructionExecutionResult.Success, 0, 0);

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

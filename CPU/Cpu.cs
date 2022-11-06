using YaNES.Core;
using YaNES.CPU.Instructions;
using YaNES.CPU.Registers;

namespace YaNES.CPU
{
    public class Cpu : ICpu, IInterruptsListener
    {
        // values are chosen to match first line of nestest.log
        private static readonly CpuInstructionExecutionReport ProcessInitialized = new(CpuInstructionExecutionResult.Success, 0, 7);

        public ICpuBus Bus => bus;
        public ICpuRegisters Registers => registers;
        public IInterruptsListener InterruptsListener => this;

        private readonly CpuSettings settings;
        private readonly Bus bus = new();
        private readonly RegistersProvider registers = new();
        private readonly InstructionsProvider instructions = new();
        private readonly bool[] interrupts = new bool[2];
        private readonly ushort[] interruptsVectors = new ushort[2] { 0xFFFE, 0xFFFA };

        public Cpu(CpuSettings settings)
        {
            this.settings = settings;
        }

        public CpuInstructionExecutionReport InsertCartridge(IRom rom)
        {
            bus.AttachRom(rom);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            return ProcessInitialized;
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

        public CpuInstructionExecutionReport ExecuteNextInstruction()
        {
            var cycles = 0;

            for (int interrupt = 0; interrupt < interrupts.Length; interrupt++)
            {
                if (interrupts[interrupt])
                {
                    cycles += HandleInterrupt((Interrupt)interrupt);
                    interrupts[interrupt] = false;
                }
            }

            var nextInstructionAddress = registers.ProgramCounter.State;
            var opcode = bus.Read8bit(nextInstructionAddress);

            registers.ProgramCounter.State++;

            if (opcode == 0)
                cycles += HandleInterrupt(Interrupt.BRK);

            var instruction = instructions.GetInstructionForOpcode(opcode);
            cycles += instruction.Execute(bus, registers);

            return new CpuInstructionExecutionReport(CpuInstructionExecutionResult.Success, opcode, cycles);
        }

        private int HandleInterrupt(Interrupt interrupt)
        {
            var programCounter = registers.ProgramCounter.State;
            var counterMostSignificantByte = (byte)(programCounter >> 8);
            var counterLeastSignificantByte = (byte)programCounter;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), counterMostSignificantByte);
            registers.StackPointer.State -= 1;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), counterLeastSignificantByte);
            registers.StackPointer.State -= 1;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), registers.ProcessorStatus.State);
            registers.StackPointer.State -= 1;

            // TODO : set BFlag or BreakCommand?

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.InterruptDisable, true);
            registers.ProgramCounter.State = bus.Read16bit(interruptsVectors[(int)interrupt]);

            return 2;
        }

        public void Trigger(Interrupt interrupt)
        {
            if (registers.ProcessorStatus.Get(ProcessorStatus.Flags.InterruptDisable) && interrupt != Interrupt.NMI)
                return;

            interrupts[(int)interrupt] = true;
        }
    }
}

using NesEmulatorCPU.Utils;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Instructions;

namespace NesEmulatorCPU
{
    internal class Cpu : ICpu
    {
        public ushort ProgramCounter => registers.ProgramCounter.State;
        public byte StackPointer => registers.StackPointer.State;
        public byte Accumulator => registers.Accumulator.State;
        public byte IndexRegisterX => registers.IndexRegisterX.State;
        public byte IndexRegisterY => registers.IndexRegisterY.State;
        public byte ProcessorStatus => registers.ProcessorStatus.State;

        private readonly RAM ram = new();
        private readonly RegistersProvider registers = new();
        private readonly InstructionsProvider instructions = new();

        public void Run(byte[] program)
        {
            LoadProgramToRam(program);

            SetupRegisters();
            SetupProgramCounter();
            SetupStackPointer();

            while (true)
            {
                var nextInstructionAddress = registers.ProgramCounter.State;
                var opcode = ram.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.State++;

                if (opcode == 0)
                    break;

                instructions.GetInstructionForOpcode(opcode).Execute(ram, registers);
            }
        }

        private void LoadProgramToRam(byte[] program)
        {
            for (ushort i = 0; i < program.Length; i++)
            {
                var programByte = program[i];
                var programByteAddress = (ushort)(ReservedAddresses.StartingProgramAddress + i);

                ram.Write8Bit(programByteAddress, programByte);
            }

            ram.Write16Bit(ReservedAddresses.ProgramStartPointerAddress, ReservedAddresses.StartingProgramAddress);
        }

        private void SetupRegisters() => registers.Reset();

        private void SetupProgramCounter()
        {
            registers.ProgramCounter.State = ram.Read16bit(ReservedAddresses.ProgramStartPointerAddress);
        }

        private void SetupStackPointer()
        {
            registers.StackPointer.State = ReservedAddresses.StartingStackPointerAddress;
        }
    }
}

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

        private readonly RAM ram = new();
        private readonly Registers registers = new();
        private readonly Instructions instructions = new();

        public void Run(byte[] program)
        {
            ResetRegisters();
            LoadProgramToRam(program);
            SetupProgramCounter();

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

        private void ResetRegisters() => registers.Reset();

        private void LoadProgramToRam(byte[] program)
        {
            for (ushort i = 0; i < program.Length; i++)
            {
                var programByte = program[i];
                var programAddress = (ushort)(ReservedAddresses.StartingProgramAddress + i);

                ram.Write8Bit(programAddress, programByte);
            }

            ram.Write16Bit(ReservedAddresses.ProgramStartPointerAddress, ReservedAddresses.StartingProgramAddress);
        }

        private void SetupProgramCounter()
        {
            registers.ProgramCounter.State = ram.Read16bit(ReservedAddresses.ProgramStartPointerAddress);
        }
    }
}

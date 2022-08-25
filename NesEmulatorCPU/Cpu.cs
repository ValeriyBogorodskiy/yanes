﻿using NesEmulatorCPU.Utils;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Instructions;

namespace NesEmulatorCPU
{
    public class Cpu
    {
        public IRAM RAM => ram;
        public IRegisters Registers => registers;

        private CPUSettings settings;
        private readonly RAM ram = new();
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
                var opcode = ram.Read8bit(nextInstructionAddress);

                registers.ProgramCounter.State++;

                if (opcode == 0)
                    break;

                var instruction = instructions.GetInstructionForOpcode(opcode);
                instruction.Execute(ram, registers);

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

                ram.Write8Bit(programByteAddress, programByte);
            }

            ram.Write16Bit(ReservedAddresses.ProgramStartPointerAddress, settings.StartingProgramAddress);
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

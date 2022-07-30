﻿using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class BIT : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(ram, registers);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, (value & 0b10000000) > 0);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, (value & 0b01000000) > 0);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, (value & registers.Accumulator.State) == 0);
        }
    }
}

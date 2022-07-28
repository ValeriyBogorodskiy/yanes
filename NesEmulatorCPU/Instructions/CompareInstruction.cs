﻿using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions
{
    /// <summary>
    /// http://www.6502.org/tutorials/compare_instructions.html
    /// </summary>
    internal abstract class CompareInstruction : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetAddress(ram, registers);
            var value = ram.Read8bit(valueAddress);

            var subtractionResult = (byte)(GetRegisterValue(registers) - value);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, subtractionResult.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, subtractionResult.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, subtractionResult.IsZero() || subtractionResult.IsPositive());
        }

        protected abstract byte GetRegisterValue(RegistersProvider registers);
    }
}

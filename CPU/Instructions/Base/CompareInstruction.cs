using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Base
{
    /// <summary>
    /// http://www.6502.org/tutorials/compare_instructions.html
    /// http://www.6502.org/tutorials/compare_beyond.html
    /// </summary>
    internal abstract class CompareInstruction : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var registerValue = GetRegisterValue(registers);
            var ramValue = addressingMode.GetRamValue(bus, registers);
            var subtractionResult = (byte)(registerValue - ramValue);

            if (registerValue < ramValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, subtractionResult.IsNegative());
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, false);
            }
            else if (registerValue == ramValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, true);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);
            }
            else if (registerValue > ramValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, subtractionResult.IsNegative());
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);
            }
        }

        protected abstract byte GetRegisterValue(RegistersProvider registers);
    }
}

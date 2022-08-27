using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Base
{
    internal abstract class LoadByteInstruction : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);

            TargetRegister(registers).State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }

        protected abstract CpuRegister8Bit TargetRegister(RegistersProvider registers);
    }
}

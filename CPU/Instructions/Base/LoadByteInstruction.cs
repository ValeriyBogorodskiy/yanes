using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Base
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

using YaNES.Core;
using YaNES.Core.Utils;
using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;

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

        protected abstract Register8Bit TargetRegister(RegistersProvider registers);
    }
}

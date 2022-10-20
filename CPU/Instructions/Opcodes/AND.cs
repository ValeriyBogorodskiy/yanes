using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class AND : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);
            var result = (byte)(value & registers.Accumulator.State);

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
        }
    }
}

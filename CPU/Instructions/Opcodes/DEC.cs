using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class DEC : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var newValue = (byte)(value - 1);

            bus.Write8Bit(valueAddress, newValue);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, newValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, newValue.IsZero());
        }
    }
}

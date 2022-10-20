using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class INS : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var newValue = (byte)(value + 1);

            bus.Write8Bit(valueAddress, newValue);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, newValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, newValue.IsZero());

            var onesComplement = (byte)~newValue;
            return onesComplement;
        }
    }
}

using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Base
{
    internal abstract class StoreInstructionLogic : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetRamAddress(bus, registers);
            bus.Write8Bit(memoryAddress, GetSourceRegister(registers).State);
        }

        protected abstract CpuRegister8Bit GetSourceRegister(RegistersProvider registers);
    }
}

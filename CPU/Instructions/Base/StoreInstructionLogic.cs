using YaNES.Core;
using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Base
{
    internal abstract class StoreInstructionLogic : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetRamAddress(bus, registers);
            bus.Write8Bit(memoryAddress, GetSourceRegister(registers).State);
        }

        protected abstract Register8Bit GetSourceRegister(RegistersProvider registers);
    }
}

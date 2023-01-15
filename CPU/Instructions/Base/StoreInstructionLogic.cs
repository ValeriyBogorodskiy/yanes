using YaNES.Core;
using YaNES.Core.Utils;
using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Base
{
    internal abstract class StoreInstructionLogic : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetRamAddress(bus, registers);

            // TODO : this logic depends on mapper. With NROM it's a NOP
            if (!memoryAddress.InRange(ReservedAddresses.PrgAddressSpace))
            {
                bus.Write8Bit(memoryAddress, GetSourceRegister(registers).State);
            }
        }

        protected abstract Register8Bit GetSourceRegister(RegistersProvider registers);
    }
}

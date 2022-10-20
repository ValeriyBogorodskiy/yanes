using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class JSR : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetRamAddress(bus, registers);

            var currentAddress = registers.ProgramCounter.State - 1;

            var mostSignificantByte = (byte)(currentAddress >> 8);
            var leastSignificantByte = (byte)currentAddress;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), mostSignificantByte);

            registers.StackPointer.State -= 1;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), leastSignificantByte);

            registers.StackPointer.State -= 1;

            registers.ProgramCounter.State = memoryAddress;
        }
    }
}

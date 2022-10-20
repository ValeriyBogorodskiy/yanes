using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions
{
    internal interface IInstructionLogicWithAddressingMode
    {
        internal void Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers);
    }
}

using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions
{
    internal interface IInstructionLogic
    {
        internal void Execute(Bus bus, RegistersProvider registers);
    }
}

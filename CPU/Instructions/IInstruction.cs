using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions
{
    internal interface IInstruction
    {
        internal byte Opcode { get; }
        internal int Execute(Bus bus, RegistersProvider registers);
    }
}

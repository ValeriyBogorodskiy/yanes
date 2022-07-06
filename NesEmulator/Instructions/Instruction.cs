namespace NesEmulator.Instructions
{
    internal abstract class Instruction
    {
        internal abstract byte Opcode { get; }
        internal abstract void Execute();
    }
}

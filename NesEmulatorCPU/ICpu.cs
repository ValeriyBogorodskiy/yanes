namespace NesEmulatorCPU
{
    public interface ICpu
    {
        void Run(byte[] program);

        ushort ProgramCounter { get; }
        byte StackPointer { get; }
        byte Accumulator { get; }
        byte IndexRegisterX { get; }
        byte IndexRegisterY { get; }
        byte ProcessorStatus { get; }
    }
}

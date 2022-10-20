namespace YaNES.CPU
{
    public interface IRegisters
    {
        ushort ProgramCounter { get; set; }
        byte StackPointer { get; set; }
        byte Accumulator { get; set; }
        byte IndexRegisterX { get; set; }
        byte IndexRegisterY { get; set; }
        byte ProcessorStatus { get; set; }
    }
}

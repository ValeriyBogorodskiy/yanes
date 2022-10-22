namespace YaNES.Core
{
    public interface IPpu
    {
        IPpuRegisters Registers { get; }
        void AttachRom(IRom rom);
    }
}

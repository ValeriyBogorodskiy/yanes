namespace YaNES.Core
{
    public interface IPpuRegisters
    {
        byte Controller { set; }
        byte Mask { set; }
        byte Status { get; }
        byte OamAddress { set; }
        byte OamData { get; }
        byte Scroll { set; }
        byte Address { set; }
        byte Data { get; set; }
        byte OpenBus { get; }
    }
}

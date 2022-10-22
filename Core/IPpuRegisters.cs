namespace YaNES.Core
{
    public interface IPpuRegisters
    {
        byte Controller { get; set; }
        byte Mask { get; set; }
        byte Status { get; set; }
        byte OamAddress { get; set; }
        byte OamData { get; set; }
        byte Scroll { get; set; }
        byte Address { get; set; }
        byte Data { get; set; }
    }
}

namespace YaNES.CPU
{
    public class CpuSettings
    {
        public ushort StartingProgramAddress { get; set; }
        public byte InitialProcessorStatus { get; set; }

        public static CpuSettings Default => new()
        {
            StartingProgramAddress = 0x8000,
            InitialProcessorStatus = 0
        };
    }
}

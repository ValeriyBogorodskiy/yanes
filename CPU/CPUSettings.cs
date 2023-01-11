namespace YaNES.CPU
{
    public class CpuSettings
    {
        public byte InitialProcessorStatus { get; set; }

        public static CpuSettings Default => new()
        {
            InitialProcessorStatus = 0
        };
    }
}

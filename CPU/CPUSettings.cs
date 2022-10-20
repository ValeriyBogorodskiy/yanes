namespace NesEmulatorCPU
{
    public class CPUSettings
    {
        public ushort StartingProgramAddress { get; set; }
        public byte InitialProcessorStatus { get; set; }

        public static CPUSettings Default => new()
        {
            StartingProgramAddress = 0x8000,
            InitialProcessorStatus = 0
        };
    }
}

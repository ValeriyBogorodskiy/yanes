namespace NesEmulatorCPU
{
    public class CPUSettings
    {
        public ushort StartingProgramAddress { get; set; }

        public static CPUSettings Default => new()
        {
            StartingProgramAddress = 0x8000
        };
    }
}

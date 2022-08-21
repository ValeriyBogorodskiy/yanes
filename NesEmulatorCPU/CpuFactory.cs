namespace NesEmulatorCPU
{
    public class CpuFactory
    {
        public static ICpu CreateCpu()
        {
            return new Cpu();
        }
    }
}

namespace NesEmulatorCPU.Utils
{
    internal static class Extensions
    {
        public static bool IsNegative(this byte value) => (value & BitMasks.Negative) != 0;

        public static bool IsZero(this byte value) => value == 0;

        public static bool IsPositive(this byte value) => value > 0 && value < 128;

        public static byte ToComplimentaryNegative(this byte value) => (byte)(~value + 1);

        public static byte ToComplimentaryPositive(this byte value) => (byte)(~value + 1);
    }
}

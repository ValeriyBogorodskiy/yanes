namespace NesEmulatorCPU.Utils
{
    internal static class Extensions
    {
        public static bool IsNegative(this byte value) => (value & BitMasks.Negative) != 0;

        public static bool IsZero(this byte value) => value == 0;

        // TODO : tests https://codebase64.org/doku.php?id=base:two_s_complement_system
        public static byte ToComplimentaryNegative(this byte value) => (byte)(~value + 1);

        // TODO : tests https://codebase64.org/doku.php?id=base:two_s_complement_system
        public static byte ToComplimentaryPositive(this byte value) => (byte)(~value + 1);
    }
}

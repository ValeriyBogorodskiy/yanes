namespace YaNES.Core.Utils
{
    public static class Extensions
    {
        public static bool IsNegative(this byte value) => value >= 0b1000_0000;

        public static bool IsZero(this byte value) => value == 0;

        public static bool IsPositive(this byte value) => value > 0 && value < 0b1000_0000;

        public static byte ToComplimentaryNegative(this byte value) => (byte)(~value + 1);

        public static byte ToComplimentaryPositive(this byte value) => (byte)(~value + 1);

        public static bool InRange(this ushort value, ushort min, ushort max)
        {
            return value >= min && value <= max;
        }

        public static bool InRange(this ushort value, AddressSpace addressSpace)
        {
            return value >= addressSpace.Start && value <= addressSpace.End;
        }
    }
}

namespace NesEmulatorCPU.Utils
{
    internal static class ComplimentaryMath
    {
        /// <summary>
        /// https://www.quora.com/How-do-I-subtract-using-2%E2%80%B2s-compliment
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal static byte Subtract2sCompliments(byte lhs, byte rhs)
        {
            var rhsComplimentary = rhs.ToComplimentaryPositive();
            var resultWithCarry = lhs + rhsComplimentary;
            return (byte)(resultWithCarry & 0b1111_1111);
        }
    }
}

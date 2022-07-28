using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Test
{
    [TestFixture]
    internal class ComplimentaryMath
    {
        [Test]
        public void PositiveMinusPositiveResultPositive()
        {
            byte lhs = 10;
            byte rhs = 9;

            var straightforwardResult = (byte)(lhs - rhs);
            var complimentaryResult = Utils.ComplimentaryMath.Subtract2sCompliments(lhs, rhs);

            Assert.AreEqual(1, complimentaryResult);
            Assert.AreEqual(complimentaryResult, straightforwardResult);
        }

        [Test]
        public void PositiveMinusPositiveResultNegative()
        {
            byte lhs = 10;
            byte rhs = 11;

            byte straightforwardResult = (byte)(lhs - rhs);
            var complimentaryResult = Utils.ComplimentaryMath.Subtract2sCompliments(lhs, rhs);

            Assert.AreEqual(255, complimentaryResult);
            Assert.AreEqual(complimentaryResult, straightforwardResult);
        }

        [Test]
        public void PositiveMinusNegativeResultPositive()
        {
            byte lhs = 10;
            byte rhs = 15;
            rhs = rhs.ToComplimentaryNegative();

            var straightforwardResult = (byte)(lhs - rhs);
            var complimentaryResult = Utils.ComplimentaryMath.Subtract2sCompliments(lhs, rhs);

            Assert.AreEqual(25, complimentaryResult);
            Assert.AreEqual(complimentaryResult, straightforwardResult);
        }

        [Test]
        public void NegativeMinusPositiveResultNegative()
        {
            byte lhs = 50;
            lhs = lhs.ToComplimentaryNegative();
            byte rhs = 15;

            var straightforwardResult = (byte)(lhs - rhs);
            var complimentaryResult = Utils.ComplimentaryMath.Subtract2sCompliments(lhs, rhs);

            Assert.AreEqual(0b1011_1111, complimentaryResult);
            Assert.AreEqual(complimentaryResult, straightforwardResult);
        }

        [Test]
        public void NegativeMinusNegativeResultPositive()
        {
            byte lhs = 50;
            lhs = lhs.ToComplimentaryNegative();
            byte rhs = 100;
            rhs = rhs.ToComplimentaryNegative();

            var straightforwardResult = (byte)(lhs - rhs);
            var complimentaryResult = Utils.ComplimentaryMath.Subtract2sCompliments(lhs, rhs);

            Assert.AreEqual(50, complimentaryResult);
            Assert.AreEqual(complimentaryResult, straightforwardResult);
        }

        [Test]
        public void PositivePlusNegativeResultPositive()
        {
            byte lhs = 50;
            byte rhs = 25;
            rhs = rhs.ToComplimentaryNegative();

            var straightforwardResult = (byte)(lhs + rhs);

            Assert.AreEqual(25, straightforwardResult);
        }

        [Test]
        public void PositivePlusNegativeResultNegative()
        {
            byte lhs = 50;
            byte rhs = 75;
            rhs = rhs.ToComplimentaryNegative();

            var straightforwardResult = (byte)(lhs + rhs);

            Assert.AreEqual(0b1110_0111, straightforwardResult);
        }

        [Test]
        public void ComplimentaryConversions()
        {
            byte value = 64;
            byte complimentaryNegative = value.ToComplimentaryNegative();
            byte complimentaryPositive = complimentaryNegative.ToComplimentaryPositive();

            Assert.AreEqual(0b1100_0000, complimentaryNegative);
            Assert.AreEqual(0b0100_0000, complimentaryPositive);
        }
    }
}

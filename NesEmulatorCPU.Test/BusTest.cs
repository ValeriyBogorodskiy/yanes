namespace NesEmulatorCPU.Test
{
    [TestFixture]
    internal class BusTest
    {
        [Test]
        public void CpuRamIsMirroredCorrectly()
        {
            byte value = 10;
            var bus = new Bus();

            bus.Write8Bit(0x00FF, value);

            Assert.That(bus.Read8bit(0x00FF), Is.EqualTo(value));
            Assert.That(bus.Read8bit(0x08FF), Is.EqualTo(value));
            Assert.That(bus.Read8bit(0x10FF), Is.EqualTo(value));
            Assert.That(bus.Read8bit(0x18FF), Is.EqualTo(value));
        }
    }
}

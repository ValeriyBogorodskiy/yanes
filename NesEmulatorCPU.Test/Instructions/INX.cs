namespace NesEmulatorCPU.Test.Instructions
{
    // TODO : rewrite using internal classes
    [TestFixture]
    internal class INX
    {
        [Test]
        public void IncrementWithPositiveResult()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0x3A, 0xAA, 0xE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX, Is.EqualTo(0x3B));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b00000000));
        }

        [Test]
        public void IncrementWithNegativeResult()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0xBA, 0xAA, 0xE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX, Is.EqualTo(0xBB));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b10000000));
        }

        [Test]
        public void IncrementWithOverflow()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0xFF, 0xAA, 0xE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX, Is.EqualTo(0x00));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b00000010));
        }
    }
}

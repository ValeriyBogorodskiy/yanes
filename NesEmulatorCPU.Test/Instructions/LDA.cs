namespace NesEmulatorCPU.Test.Instructions
{
    // TODO : rewrite using internal classes
    [TestFixture]
    internal class LDA
    {
        [Test]
        public void LoadingZeroValue()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0x00, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.Accumulator, Is.EqualTo(0x00));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b00000010));
        }

        [Test]
        public void LoadingPositiveValue()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0x7F, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.Accumulator, Is.EqualTo(0x7F));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b00000000));
        }

        [Test]
        public void LoadingNegativeValue()
        {
            ICpu cpu = new Cpu();
            var program = new byte[] { 0xA9, 0xAA, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.Accumulator, Is.EqualTo(0xAA));
            Assert.That(cpu.ProcessorStatus, Is.EqualTo(0b10000000));
        }
    }
}

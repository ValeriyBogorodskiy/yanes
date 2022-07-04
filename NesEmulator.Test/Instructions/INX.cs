using NesEmulator.Cpu;

namespace NesEmulator.Test.Instructions
{
    [TestFixture]
    internal class INX
    {
        [Test]
        public void IncrementWithPositiveResult()
        {
            ICpu cpu = new Cpu.Cpu();
            var program = new byte[] { 0xA9, 0x3A, 0XAA, 0XE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX.State, Is.EqualTo(0X3B));
            Assert.That(cpu.ProcessorStatus.State, Is.EqualTo(0b00000000));
        }

        [Test]
        public void IncrementWithNegativeResult()
        {
            ICpu cpu = new Cpu.Cpu();
            var program = new byte[] { 0xA9, 0xBA, 0XAA, 0XE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX.State, Is.EqualTo(0XBB));
            Assert.That(cpu.ProcessorStatus.State, Is.EqualTo(0b10000000));
        }

        [Test]
        public void IncrementWithOverflow()
        {
            ICpu cpu = new Cpu.Cpu();
            var program = new byte[] { 0xA9, 0XFF, 0XAA, 0XE8, 0x00 };

            cpu.Run(program);

            Assert.That(cpu.IndexRegisterX.State, Is.EqualTo(0X00));
            Assert.That(cpu.ProcessorStatus.State, Is.EqualTo(0b00000010));
        }
    }
}

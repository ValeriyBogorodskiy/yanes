using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BITLogic
    {
        [Test]
        public void NegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b10000000);
            registers.Accumulator.State = 0b11111111;

            var bit = (IInstructionLogicWithAddressingMode)new BIT();
            bit.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b11111111));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void OverflowResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b01000000);
            registers.Accumulator.State = 0b11111111;

            var bit = (IInstructionLogicWithAddressingMode)new BIT();
            bit.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b11111111));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void ZeroResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b00000000);
            registers.Accumulator.State = 0b11111111;

            var bit = (IInstructionLogicWithAddressingMode)new BIT();
            bit.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b11111111));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }
    }
}

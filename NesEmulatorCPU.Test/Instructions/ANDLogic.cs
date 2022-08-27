using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class ANDLogic
    {
        [Test]
        public void PositiveResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b00001111);
            registers.Accumulator.State = 0b00001010;

            var and = (IInstructionLogicWithAddressingMode)new AND();
            and.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00001010));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void ZeroResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b01010101);
            registers.Accumulator.State = 0b10101010;

            var and = (IInstructionLogicWithAddressingMode)new AND();
            and.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00000000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void NegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0b11000000);
            registers.Accumulator.State = 0b11111111;

            var and = (IInstructionLogicWithAddressingMode)new AND();
            and.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b11000000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}

using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class RORLogic
    {
        [Test]
        public void PositiveResultWithCarry()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b1011_0001;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, false);

            var ror = (IInstructionLogic)new RORAccumulator();
            ror.Execute(bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b0101_1000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void NegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b0111_0000;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);

            var ror = (IInstructionLogic)new RORAccumulator();
            ror.Execute(bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b1011_1000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }
    }
}

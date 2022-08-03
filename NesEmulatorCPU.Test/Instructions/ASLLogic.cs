using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class ASLLogic
    {
        [Test]
        public void PositiveResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b00001111;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00011110));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void PositiveResultWithCarry()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b10001111;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00011110));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void NegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b01001111;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b10011110));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void NegativeResultWithCarry()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b11001111;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b10011110));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void ZeroResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b00000000;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00000000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void ZeroResultWithCarry()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0b10000000;

            var asl = (IInstructionLogic)new ASLAccumulator();
            asl.Execute(ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b00000000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }
    }
}

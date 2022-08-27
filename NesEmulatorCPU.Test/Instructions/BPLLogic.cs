using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BPLLogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, true);
            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x602));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTakenPositive()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x603));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenNegative()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0xFE);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x600));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenPageCrossedPositive()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x10FE;
            bus.Write8Bit(0x10FE, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1100));
            Assert.That(cycles, Is.EqualTo(4));
        }

        [Test]
        public void BranchTakenPageCrossedNegative()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x10FF;
            bus.Write8Bit(0x10FF, 0xFF);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x10FF));
            Assert.That(cycles, Is.EqualTo(4));
        }
    }
}

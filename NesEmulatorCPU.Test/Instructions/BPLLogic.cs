using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BPLLogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, true);
            registers.ProgramCounter.State = 0x6001;
            ram.Write8Bit(0x6001, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTakenPositive()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x6001;
            ram.Write8Bit(0x6001, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6003));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenNegative()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x6001;
            ram.Write8Bit(0x6001, 0xFE);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6000));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenPageCrossedPositive()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x60FE;
            ram.Write8Bit(0x60FE, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6100));
            Assert.That(cycles, Is.EqualTo(4));
        }

        [Test]
        public void BranchTakenPageCrossedNegative()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x60FF;
            ram.Write8Bit(0x60FF, 0xFF);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x60FF));
            Assert.That(cycles, Is.EqualTo(4));
        }
    }
}

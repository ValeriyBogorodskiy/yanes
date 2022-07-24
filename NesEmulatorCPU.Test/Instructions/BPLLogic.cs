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
            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bpl = (IInstruction)new BPL(0X10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTakenPositive()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bpl = (IInstruction)new BPL(0X10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6003));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenNegative()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0XFE);

            var bpl = (IInstruction)new BPL(0X10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6000));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenPageCrossedPositive()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0X60FE;
            ram.Write8Bit(0X60FE, 0X01);

            var bpl = (IInstruction)new BPL(0X10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6100));
            Assert.That(cycles, Is.EqualTo(4));
        }

        [Test]
        public void BranchTakenPageCrossedNegative()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0X60FF;
            ram.Write8Bit(0X60FF, 0XFF);

            var bpl = (IInstruction)new BPL(0X10);
            var cycles = bpl.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X60FF));
            Assert.That(cycles, Is.EqualTo(4));
        }
    }
}

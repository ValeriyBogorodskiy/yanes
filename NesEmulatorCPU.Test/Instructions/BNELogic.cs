using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BNELogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, true);
            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bne = (IInstruction)new BNE(0X10);
            var cycles = bne.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTaken()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bne = (IInstruction)new BNE(0X10);
            var cycles = bne.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6003));
            Assert.That(cycles, Is.EqualTo(3));
        }
    }
}

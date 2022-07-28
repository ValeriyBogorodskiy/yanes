using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BMILogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, false);
            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bmi = (IInstruction)new BMI(0X10);
            var cycles = bmi.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTaken()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, true);
            registers.ProgramCounter.State = 0X6001;
            ram.Write8Bit(0X6001, 0X01);

            var bmi = (IInstruction)new BMI(0X10);
            var cycles = bmi.Execute(ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X6003));
            Assert.That(cycles, Is.EqualTo(3));
        }
    }
}

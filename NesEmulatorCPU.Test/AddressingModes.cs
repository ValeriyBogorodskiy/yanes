using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test
{
    /// <summary>
    /// Test cases source http://www.emulator101.com/6502-addressing-modes.html
    /// </summary>
    [TestFixture]
    internal class AddressingModes
    {

        [Test]
        public void Immediate()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x8000;

            var immediate = new Immediate();
            var address = immediate.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x8000));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x8001));
        }

        [Test]
        public void ZeroPage()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9000;
            ram.Write8Bit(0x9000, 0xAA);

            var zeroPage = new ZeroPage();
            var adress = zeroPage.GetAddress(ram, registers);

            Assert.That(adress, Is.EqualTo(0xAA));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9001));
        }

        [Test]
        public void ZeroPageX()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9000;
            ram.Write8Bit(0x9000, 0xC0);
            registers.IndexRegisterX.State = 0X60;

            var zeroPageX = new ZeroPageX();
            var address = zeroPageX.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x0020));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9001));
        }

        [Test]
        public void ZeroPageY()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9500;
            ram.Write8Bit(0x9500, 0xFA);
            registers.IndexRegisterY.State = 0XFF;

            var zeroPageY = new ZeroPageY();
            var address = zeroPageY.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x00F9));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9501));
        }

        [Test]
        public void Absolute()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9F00;
            ram.Write16Bit(0x9F00, 0x1234);

            var absolute = new Absolute();
            var address = absolute.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x1234));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9F02));
        }

        [Test]
        public void AbsoluteX()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9FF0;
            ram.Write16Bit(0x9FF0, 0x4032);
            registers.IndexRegisterX.State = 0x01;

            var absoluteX = new AbsoluteX();
            var address = absoluteX.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x4033));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9FF2));
        }

        [Test]
        public void AbsoluteXBoundaryCrossing()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            ram.Write16Bit(0x0000, 0X00FF);
            registers.IndexRegisterX.State = 0x01;

            var absoluteX = new AbsoluteX();
            var absoluteXBoundaryCrossing = (IBoundaryCrossingMode)absoluteX;

            Assert.That(absoluteXBoundaryCrossing.CheckIfBoundaryWillBeCrossed(ram, registers), Is.True);

            var address = absoluteX.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x0100));
        }

        [Test]
        public void AbsoluteY()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x9FF0;
            ram.Write16Bit(0x9FF0, 0x000F);
            registers.IndexRegisterY.State = 0xFF;

            var absoluteY = new AbsoluteY();
            var address = absoluteY.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x010E));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x9FF2));
        }

        [Test]
        public void AbsoluteYBoundaryCrossing()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            ram.Write16Bit(0x0000, 0X00FF);
            registers.IndexRegisterY.State = 0x01;

            var absoluteY = new AbsoluteY();
            var absoluteYBoundaryCrossing = (IBoundaryCrossingMode)absoluteY;

            Assert.That(absoluteYBoundaryCrossing.CheckIfBoundaryWillBeCrossed(ram, registers), Is.True);

            var address = absoluteY.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x0100));
        }

        [Test]
        public void Indirect()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1000;
            ram.Write8Bit(0x1000, 0x52);
            ram.Write8Bit(0x1001, 0x3A);

            var indirect = new Indirect();
            var address = indirect.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x3A52));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1002));
        }

        [Test]
        public void IndexedInderect()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            ram.Write8Bit(0x0000, 0x20);
            registers.IndexRegisterX.State = 0x04;
            ram.Write16Bit(0x0024, 0x2074);

            var indexedInderect = new IndirectX();
            var address = indexedInderect.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x2074));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        [Test]
        public void IndirectIndexed()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            ram.Write8Bit(0x0000, 0x86);
            ram.Write16Bit(0x0086, 0x4028);
            registers.IndexRegisterY.State = 0x10;

            var indirectIndexed = new IndirectY();
            var address = indirectIndexed.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x4038));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        /// <summary>
        /// https://skilldrick.github.io/easy6502/#addressing
        /// </summary>
        [Test]
        public void IndirectIndexed_2()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            ram.Write8Bit(0x0000, 0x01);

            registers.IndexRegisterY.State = 0x01;
            ram.Write8Bit(0x01, 0x03);
            ram.Write8Bit(0x02, 0x07);

            var indirectIndexed = new IndirectY();
            var address = indirectIndexed.GetAddress(ram, registers);

            Assert.That(address, Is.EqualTo(0x0704));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        [Test]
        public void IndirectIndexedBoundaryCrossing()
        {
            // TODO : implement
        }
    }
}

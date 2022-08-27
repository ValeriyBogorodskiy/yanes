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
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x8000;

            var immediate = new Immediate();
            var address = immediate.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x8000));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x8001));
        }

        [Test]
        public void ZeroPage()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1000;
            bus.Write8Bit(0x1000, 0xAA);

            var zeroPage = new ZeroPage();
            var adress = zeroPage.GetRamAddress(bus, registers);

            Assert.That(adress, Is.EqualTo(0xAA));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1001));
        }

        [Test]
        public void ZeroPageX()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1000;
            bus.Write8Bit(0x1000, 0xC0);
            registers.IndexRegisterX.State = 0x60;

            var zeroPageX = new ZeroPageX();
            var address = zeroPageX.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x0020));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1001));
        }

        [Test]
        public void ZeroPageY()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1500;
            bus.Write8Bit(0x1500, 0xFA);
            registers.IndexRegisterY.State = 0xFF;

            var zeroPageY = new ZeroPageY();
            var address = zeroPageY.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x00F9));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1501));
        }

        [Test]
        public void Absolute()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1F00;
            bus.Write16Bit(0x1F00, 0x1234);

            var absolute = new Absolute();
            var address = absolute.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x1234));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1F02));
        }

        [Test]
        public void AbsoluteX()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x1FF0;
            bus.Write16Bit(0x1FF0, 0x1032);
            registers.IndexRegisterX.State = 0x01;

            var absoluteX = new AbsoluteX();
            var address = absoluteX.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x1033));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x1FF2));
        }

        [Test]
        public void AbsoluteXBoundaryCrossing()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            bus.Write16Bit(0x0000, 0x00FF);
            registers.IndexRegisterX.State = 0x01;

            var absoluteX = new AbsoluteX();
            var absoluteXBoundaryCrossing = (IBoundaryCrossingMode)absoluteX;

            Assert.That(absoluteXBoundaryCrossing.CheckIfBoundaryWillBeCrossed(bus, registers), Is.True);

            var address = absoluteX.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x0100));
        }

        [Test]
        public void AbsoluteY()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0xFF0;
            bus.Write16Bit(0xFF0, 0x000F);
            registers.IndexRegisterY.State = 0xFF;

            var absoluteY = new AbsoluteY();
            var address = absoluteY.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x010E));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0xFF2));
        }

        [Test]
        public void AbsoluteYBoundaryCrossing()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            bus.Write16Bit(0x0000, 0x00FF);
            registers.IndexRegisterY.State = 0x01;

            var absoluteY = new AbsoluteY();
            var absoluteYBoundaryCrossing = (IBoundaryCrossingMode)absoluteY;

            Assert.That(absoluteYBoundaryCrossing.CheckIfBoundaryWillBeCrossed(bus, registers), Is.True);

            var address = absoluteY.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x0100));
        }

        [Test]
        public void Indirect()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            bus.Write16Bit(0x00, 0x500);
            bus.Write8Bit(0x500, 0x52);
            bus.Write8Bit(0x501, 0x1A);

            var indirect = new Indirect();
            var address = indirect.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x1A52));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0002));
        }

        [Test]
        public void IndexedInderect()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            bus.Write8Bit(0x0000, 0x20);
            registers.IndexRegisterX.State = 0x04;
            bus.Write16Bit(0x0024, 0x2074);

            var indexedInderect = new IndirectX();
            var address = indexedInderect.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x2074));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        [Test]
        public void IndirectIndexed()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            bus.Write8Bit(0x0000, 0x86);
            bus.Write16Bit(0x0086, 0x4028);
            registers.IndexRegisterY.State = 0x10;

            var indirectIndexed = new IndirectY();
            var address = indirectIndexed.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x4038));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        /// <summary>
        /// https://skilldrick.github.io/easy6502/#addressing
        /// </summary>
        [Test]
        public void IndirectIndexed_2()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            bus.Write8Bit(0x0000, 0x01);

            registers.IndexRegisterY.State = 0x01;
            bus.Write8Bit(0x01, 0x03);
            bus.Write8Bit(0x02, 0x07);

            var indirectIndexed = new IndirectY();
            var address = indirectIndexed.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x0704));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }

        [Test]
        public void IndirectIndexedBoundaryCrossing()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x0000;

            bus.Write8Bit(0x0000, 0x01);

            registers.IndexRegisterY.State = 0x02;
            bus.Write8Bit(0x01, 0xFF);
            bus.Write8Bit(0x02, 0x01);

            var indirectIndexed = new IndirectY();
            var indirectIndexedBoundaryCrossing = (IBoundaryCrossingMode)indirectIndexed;

            Assert.That(indirectIndexedBoundaryCrossing.CheckIfBoundaryWillBeCrossed(bus, registers), Is.True);

            var address = indirectIndexed.GetRamAddress(bus, registers);

            Assert.That(address, Is.EqualTo(0x0201));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0001));
        }
    }
}

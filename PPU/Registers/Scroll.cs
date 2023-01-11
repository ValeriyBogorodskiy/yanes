namespace YaNES.PPU.Registers
{
    internal class Scroll
    {
        public byte ScrollX { get; private set; }
        public byte ScrollY { get; private set; }

        private bool latch = true;

        public byte State
        {
            set
            {
                if (latch)
                    ScrollX = value;
                else
                    ScrollY = value;

                latch = !latch;
            }
        }

        public void ResetLatch()
        {
            latch = true;
        }
    }
}

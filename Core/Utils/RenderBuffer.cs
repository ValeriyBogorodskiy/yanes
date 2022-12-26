namespace YaNES.Core.Utils
{
    public class RenderBuffer
    {
        private const int BytesPerPixel = 3;

        private readonly int imageWidth;
        private readonly int imageHeight;
        private readonly bool mirrorX;
        private readonly bool mirrorY;

        public byte[] Pixels { get; }

        public RenderBuffer(int imageWidth, int imageHeight, bool mirrorX, bool mirrorY)
        {
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;

            this.mirrorX = mirrorX;
            this.mirrorY = mirrorY;

            Pixels = new byte[imageWidth * imageHeight * BytesPerPixel];
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (mirrorX)
                x = imageWidth - 1 - x;

            if (mirrorY)
                y = imageHeight - 1 - y;

            var rIndex = (x * BytesPerPixel) + y * imageWidth * BytesPerPixel;
            var gIndex = rIndex + 1;
            var bIndex = gIndex + 1;

            Pixels[rIndex] = r;
            Pixels[gIndex] = g;
            Pixels[bIndex] = b;
        }
    }
}

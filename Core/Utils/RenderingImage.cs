namespace YaNES.Core.Utils
{
    public class RenderingImage
    {
        private const int BytesPerPixel = 3;

        private readonly int imageWidth;
        private readonly int imageHeight;

        public byte[] Pixels { get; }

        public RenderingImage(int imageWidth, int imageHeight)
        {
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;

            Pixels = new byte[imageWidth * imageHeight * BytesPerPixel];
        }

        // TODO : make mirroring optional and apply it for both axes
        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            var mirroredY = imageHeight - 1 - y;
            var rIndex = (x * BytesPerPixel) + mirroredY * imageWidth * BytesPerPixel;
            var gIndex = rIndex + 1;
            var bIndex = gIndex + 1;

            Pixels[rIndex] = r;
            Pixels[gIndex] = g;
            Pixels[bIndex] = b;
        }
    }
}

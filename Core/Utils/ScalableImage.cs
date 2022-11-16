namespace YaNES.Core.Utils
{
    public class ScalableImage
    {
        private const int BytesPerPixel = 3;

        private readonly int originalImageWidth;
        private readonly int originalImageHeight;
        private readonly int scaleFactor;

        public byte[] OriginalImage { get; }
        public byte[] ScaledImage { get; }

        public ScalableImage(int originalImageWidth, int originalImageHeight, int scaleFactor)
        {
            this.originalImageWidth = originalImageWidth;
            this.originalImageHeight = originalImageHeight;
            this.scaleFactor = scaleFactor;

            OriginalImage = new byte[originalImageWidth * originalImageHeight * BytesPerPixel];
            ScaledImage = new byte[OriginalImage.Length * scaleFactor * scaleFactor];
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            var rIndex = (x * BytesPerPixel) + y * originalImageWidth * BytesPerPixel;
            var gIndex = rIndex + 1;
            var bIndex = gIndex + 1;

            OriginalImage[rIndex] = r;
            OriginalImage[gIndex] = g;
            OriginalImage[bIndex] = b;
        }

        public void Scale()
        {
            for (int i = 0; i < originalImageWidth * originalImageHeight; i++)
            {
                var rIndex = i * BytesPerPixel;

                var r = OriginalImage[rIndex];
                var g = OriginalImage[rIndex + 1];
                var b = OriginalImage[rIndex + 2];

                var x = (i % originalImageWidth) * scaleFactor;
                var y = (i / originalImageWidth) * scaleFactor;

                for (int pixelX = 0; pixelX < scaleFactor; pixelX++)
                {
                    for (int pixelY = 0; pixelY < scaleFactor; pixelY++)
                    {
                        var rIndexScaled = (x + pixelX) * BytesPerPixel + (y + pixelY) * BytesPerPixel * originalImageWidth * scaleFactor;
                        var gIndexScaled = rIndexScaled + 1;
                        var bIndexScaled = rIndexScaled + 2;

                        ScaledImage[rIndexScaled] = r;
                        ScaledImage[gIndexScaled] = g;
                        ScaledImage[bIndexScaled] = b;
                    }
                }
            }
        }
    }
}

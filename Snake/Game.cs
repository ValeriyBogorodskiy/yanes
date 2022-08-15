using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Snake
{
    internal class Game : GameWindow
    {
        private Color4 backgroundColor;

        public Game() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
        }

        public void SetBackgroundColor(Color4 color) => backgroundColor = color;

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(backgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            this.Context.SwapBuffers();
        }
    }
}

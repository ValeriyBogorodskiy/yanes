using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace YaNES.Console
{
    internal class YanesWindow : GameWindow
    {
        public YanesWindow(int frameRate, int width, int height) : base(
            new GameWindowSettings()
            {
                RenderFrequency = frameRate,
                UpdateFrequency = frameRate
            },
            new NativeWindowSettings() // TODO : override native settings for macOS
            {
                Size = new Vector2i(width, height)
            })
        {
        }
    }
}

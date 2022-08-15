using Snake;
using OpenTK.Mathematics;

using (Game game = new Game())
{
    game.KeyDown += args => KeyDownHandler(game, args);
    game.Run();
}

void KeyDownHandler(Game game, OpenTK.Windowing.Common.KeyboardKeyEventArgs args)
{
    switch (args.Key)
    {
        case OpenTK.Windowing.GraphicsLibraryFramework.Keys.R:
            game.SetBackgroundColor(new Color4(1f, 0f, 0f, 0f));
            break;
        case OpenTK.Windowing.GraphicsLibraryFramework.Keys.G:
            game.SetBackgroundColor(new Color4(0f, 1f, 0f, 0f));
            break;
        case OpenTK.Windowing.GraphicsLibraryFramework.Keys.B:
            game.SetBackgroundColor(new Color4(0f, 0f, 1f, 0f));
            break;
    }
}
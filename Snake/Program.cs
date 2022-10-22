using OpenTK.Windowing.Common;
using YaNES.Core;
using YaNES.CPU;
using YaNES.ROM;
using YaNES.Snake;

var rom = RomParser.FromFile("../../../snake.nes");
var cpuSettings = new CpuSettings() { StartingProgramAddress = 0x8600 };
var cpu = new CPU(cpuSettings);
var cpuProcess = cpu.Run(rom);

var frameRate = 120;
var instructionsPerSecond = 3000;
var screenSize = 32;
var screenScaleFactor = 15;
var scaledScreenSize = screenSize * screenScaleFactor;
var bytesPerPixel = 3;
var scaledImage = new byte[bytesPerPixel * scaledScreenSize * scaledScreenSize];
var random = new Random();

using (SnakeWindow game = new(frameRate, scaledScreenSize, scaledScreenSize))
{
    game.UpdateFrame += args => OnUpdateFrame(game, cpu, cpuProcess, args);
    game.KeyDown += args => OnKeyDown(cpu, args);
    game.Run();
}

// TODO : extract scaling logic
void OnUpdateFrame(SnakeWindow game, CPU cpu, IEnumerator<CpuInstructionExecutionReport> cpuProcess, FrameEventArgs args)
{
    var intructionsToExecute = (int)(instructionsPerSecond * args.Time);
    var randomValue = (byte)random.Next(1, 16);

    for (int i = 0; i < intructionsToExecute; i++)
    {
        cpu.Bus.Write8Bit(0xFE, randomValue);
        cpuProcess.MoveNext();
    }

    var topLeftPixelAddress = 0x0200;

    for (int i = 0; i < screenSize * screenSize; i++)
    {
        ushort pixelAddress = (ushort)(topLeftPixelAddress + i);
        byte colorByte = cpu.Bus.Read8bit(pixelAddress) == 0 ? (byte)0 : (byte)255;

        var x = (i % screenSize) * screenScaleFactor;
        var y = (i / screenSize) * screenScaleFactor;

        for (int pixelX = 0; pixelX < screenScaleFactor; pixelX++)
        {
            for (int pixelY = 0; pixelY < screenScaleFactor; pixelY++)
            {
                var r = (x + pixelX) * bytesPerPixel + (y + pixelY) * bytesPerPixel * scaledScreenSize;
                var g = r + 1;
                var b = g + 1;

                scaledImage[r] = colorByte;
                scaledImage[g] = colorByte;
                scaledImage[b] = colorByte;
            }
        }
    }

    game.SetImage(scaledImage);
}

void OnKeyDown(CPU cpu, KeyboardKeyEventArgs args)
{
    // controls are inverted because screen matrix is inverted in CPUs memory
    byte keyCode = args.Key switch
    {
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.S => 0x77,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.A => 0x61,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.W => 0x73,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.D => 0x64,
        _ => 0
    };

    if (keyCode != 0)
    {
        cpu.Bus.Write8Bit(0xFF, keyCode);
    }
}
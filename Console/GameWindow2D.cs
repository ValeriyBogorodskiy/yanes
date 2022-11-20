using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System.Reflection;

namespace YaNES.Console
{
    internal class GameWindow2D : GameWindow
    {
        private int vertexArrayObject;
        private int vertexBufferObject;

        private readonly float[] vertices = {
        //   Position 3D   Texture coordinates 2D
             1f, 1f, 0.0f, 1.0f, 1.0f, // top right
             1f, -1f, 0.0f, 1.0f, 0.0f, // bottom right
            -1f, -1f, 0.0f, 0.0f, 0.0f, // bottom left
            -1f, 1f, 0.0f, 0.0f, 1.0f  // top left
        };

        private int elementBufferObject;

        private readonly uint[] indices = {
            0, 1, 3,
            1, 2, 3
        };

        private int shaderProgram;

        private readonly Dictionary<string, int> uniformLocations = new();

        private int texture;

        private byte[]? image;
        private readonly Vector2i originalSize;

        public GameWindow2D(int frameRate, Vector2i originalSize, int scale) : base(
            new GameWindowSettings()
            {
                RenderFrequency = frameRate,
                UpdateFrequency = frameRate
            },
            new NativeWindowSettings() // TODO : override native settings for macOS
            {
                Size = originalSize * scale
            })
        {
            this.originalSize = originalSize;
        }

        // https://github.com/opentk/LearnOpenTK/blob/master/Chapter1/2-HelloTriangle/Window.cs
        // https://github.com/opentk/LearnOpenTK/blob/master/Chapter1/5-Textures/Window.cs
        protected override void OnLoad()
        {
            base.OnLoad();

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var vertexShaderSource = ReadEmbeddedResource("YaNES.Console.shader.vert");
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            var fragmentShaderSource = ReadEmbeddedResource("YaNES.Console.shader.frag");
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            shaderProgram = GL.CreateProgram();

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(shaderProgram, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(shaderProgram, i, out _, out _);
                var location = GL.GetUniformLocation(shaderProgram, key);
                uniformLocations.Add(key, location);
            }

            GL.UseProgram(shaderProgram);

            var vertexLocation = GL.GetAttribLocation(shaderProgram, "aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = GL.GetAttribLocation(shaderProgram, "aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }

        private string ReadEmbeddedResource(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(path);
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            if (image == null)
                return;

            Title = "YaNES (Vsync: " + VSync.ToString() + ") FPS: " + (1f / args.Time).ToString("0.");

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, originalSize.X, originalSize.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, image);

            // https://gdbooks.gitbooks.io/legacyopengl/content/Chapter7/MinMag.html
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(vertexArrayObject);

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.UseProgram(shaderProgram);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        public void SetImage(byte[] image)
        {
            this.image = image;
        }
    }
}

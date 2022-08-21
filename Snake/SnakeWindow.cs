using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Snake
{
    // TODO : create 2DGameWindow class based on this
    internal class SnakeWindow : GameWindow
    {
        private readonly float[] vertices =
        {
            // Position         Texture coordinates
             1f, 1f, 0.0f, 1.0f, 1.0f, // top right
             1f, -1f, 0.0f, 1.0f, 0.0f, // bottom right
            -1f, -1f, 0.0f, 0.0f, 0.0f, // bottom left
            -1f, 1f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;
        private int shaderProgram;
        private Dictionary<string, int> uniformLocations;
        private int texture;
        private byte[] image;

        public SnakeWindow(int frameRate, int width, int height) : base(
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

        internal void SetImage(byte[] image)
        {
            this.image = image;
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

            string vertexShaderSource = @"
                #version 330 core

                layout(location = 0) in vec3 aPosition;

                layout(location = 1) in vec2 aTexCoord;

                out vec2 texCoord;

                void main(void)
                {
                    texCoord = aTexCoord;

                    gl_Position = vec4(aPosition, 1.0);
                }";

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            string fragmentShaderSource = @"
                #version 330

                out vec4 outputColor;

                in vec2 texCoord;

                uniform sampler2D texture0;

                void main()
                {
                    outputColor = texture(texture0, texCoord);
                }";

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
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

            uniformLocations = new Dictionary<string, int>();

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

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Size.X, Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image);

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
    }
}

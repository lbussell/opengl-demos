// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.Input.Glfw;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using System.Drawing;

using Triangle;

const string VertexShaderSource =
    """
    #version 330 core

    layout (location = 0) in vec3 position;

    void main()
    {
        // gl_Position is a built-in GLSL output variable. The value
        // we store here will be used by OpenGL to position the vertex.
        gl_Position = vec4(position, 1.0);
    }
    """;

const string FragmentShaderSource =
    """
    #version 330 core

    out vec4 fragColor;

    void main()
    {
        // Output a solid orange color for now
        fragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
    }
    """;

// Needed for Native AOT. Usually Silk.NET does this automatically with
// reflection, but that doesn't work with Native AOT, so we have to do it
// manually.
GlfwWindowing.RegisterPlatform();
GlfwInput.RegisterPlatform();

var windowOptions = WindowOptions.Default;
var window = Window.Create(windowOptions);

GL? gl = null;
Vector2D<int> viewport = default;
LinkedShaderProgram? shaderProgram = null;
BufferObject<float>? vbo = null;
VertexArrayObject<float>? vao = null;

window.Load += () =>
{
    gl = GL.GetApi(window);

    gl.ClearColor(Color.CornflowerBlue);

    var vertexShader = new Triangle.Shader(ShaderType.VertexShader, VertexShaderSource);
    var fragmentShader = new Triangle.Shader(ShaderType.FragmentShader, FragmentShaderSource);
    shaderProgram = new ShaderProgram(vertexShader, fragmentShader).Build(gl);
    Console.WriteLine("Successfully compiled and linked shaders");

    float[] vertices = [
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f,  0.5f, 0.0f
    ];

    vbo = new BufferObject<float>(gl, BufferTargetARB.ArrayBuffer, vertices);
    vao = new VertexArrayObject<float>(gl, vbo);

    vbo.Bind();
    vao.VertexAttributePointer(index: 0, count: 3, VertexAttribPointerType.Float, 3, 0);
};

window.Update += (double deltaTime) =>
{
};

window.Render += (double deltaTime) =>
{
    if (gl is null || vao is null || shaderProgram is null)
    {
        throw new Exception("Not initialized");
    }

    viewport = window.FramebufferSize;
    gl.Viewport(viewport);

    gl.Clear(ClearBufferMask.ColorBufferBit);
    gl.UseProgram(shaderProgram.Handle);
    vao.Bind();
    gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
};

window.FramebufferResize += (Vector2D<int> newSize) =>
{
    gl?.Viewport(newSize);
};

window.Run();
window.Dispose();

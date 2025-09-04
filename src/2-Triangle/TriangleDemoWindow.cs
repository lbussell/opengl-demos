// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

using Triangle;

internal sealed class TriangleDemoWindow(
    GL gl,
    IWindow window,
    LinkedShaderProgram shader,
    VertexArrayObject<float> vao
) : IOpenGLWindow
{
    private static readonly float[] s_vertices = [
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f,  0.5f, 0.0f
    ];

    private readonly GL _gl = gl;
    private readonly IWindow _window = window;
    private readonly LinkedShaderProgram _shader = shader;
    private readonly VertexArrayObject<float> _vao = vao;

    public static TriangleDemoWindow Create(GL gl, IWindow window)
    {
        var vertexShader = new Triangle.Shader(ShaderType.VertexShader, Shaders.VertexShaderSource);
        var fragmentShader = new Triangle.Shader(ShaderType.FragmentShader, Shaders.FragmentShaderSource);
        var shaderProgram = new ShaderProgram(vertexShader, fragmentShader).Build(gl);

        var vbo = new BufferObject<float>(gl, BufferTargetARB.ArrayBuffer, s_vertices);
        var vao = new VertexArrayObject<float>(gl, vbo);

        vbo.Bind();
        vao.VertexAttributePointer(index: 0, count: 3, VertexAttribPointerType.Float, 3, 0);

        return new(gl, window, shaderProgram, vao);
    }

    public void Load()
    {
        _gl.ClearColor(Color.CornflowerBlue);
    }

    public void Render(double deltaTime)
    {
        _gl.Viewport(_window.FramebufferSize);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.UseProgram(_shader.Handle);
        _vao.Bind();
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

    public void Update(double deltaTime)
    {
    }

    public void FramebufferResize(Vector2D<int> newSize)
    {
        _gl.Viewport(newSize);
    }
}

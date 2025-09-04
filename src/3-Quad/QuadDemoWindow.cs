// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using Common;

namespace Quad;

internal sealed class QuadDemoWindow(
    GL gl,
    IWindow window,
    LinkedShaderProgram shader,
    VertexArrayObject<float> vao
) : IOpenGLWindow
{
    // Vertices for a quad made of two triangles
    private static readonly float[] s_vertices = [
        -0.5f, -0.5f, 0.0f, // 0 - bottom left
        0.5f, -0.5f, 0.0f, // 1 - bottom right
        -0.5f,  0.5f, 0.0f, // 2 - top left
        0.5f, 0.5f, 0.0f, // 3 - top right
    ];

    // Indices for two triangles that make up the quad
    private static readonly uint[] s_indices = [
        // Following right-hand rule for front face winding:
        // First triangle
        0, 1, 2,
        // Second triangle
        1, 3, 2
    ];

    private readonly GL _gl = gl;
    private readonly IWindow _window = window;
    private readonly LinkedShaderProgram _shader = shader;
    private readonly VertexArrayObject<float> _vao = vao;

    public static QuadDemoWindow Create(GL gl, IWindow window)
    {
        var vertexShader = new Common.Shader(ShaderType.VertexShader, Shaders.VertexShaderSource);
        var fragmentShader = new Common.Shader(ShaderType.FragmentShader, Shaders.FragmentShaderSource);
        var shaderProgram = new ShaderProgram(vertexShader, fragmentShader).Build(gl);

        // Vertex buffer contains all the vertices we'll use to draw the quad.
        var vertexBuffer = new BufferObject<float>(gl, BufferTargetARB.ArrayBuffer, s_vertices);
        // Sometimes also known as index buffer, the element buffer contains
        // indices that reference the vertices in the vertex buffer.
        var elementBuffer = new BufferObject<uint>(gl, BufferTargetARB.ElementArrayBuffer, s_indices);
        var vao = new VertexArrayObject<float>(gl, vertexBuffer, elementBuffer);

        vertexBuffer.Bind();
        vao.VertexAttributePointer(index: 0, count: 3, type: VertexAttribPointerType.Float, vertexSize: 3, offset: 0);

        return new(gl, window, shaderProgram, vao);
    }

    public void Load()
    {
        _gl.ClearColor(Color.CornflowerBlue);
    }

    public unsafe void Render(double deltaTime)
    {
        _gl.Viewport(_window.FramebufferSize);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.UseProgram(_shader.Handle);
        _vao.Bind();

        _gl.DrawElements(
            mode: PrimitiveType.Triangles,
            count: (uint)s_indices.Length,
            type: DrawElementsType.UnsignedInt,
            indices: (void*)0
        );
    }

    public void Update(double deltaTime)
    {
    }

    public void FramebufferResize(Vector2D<int> newSize)
    {
        _gl.Viewport(newSize);
    }
}

// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Common;

public sealed class OpenGLWindowHost
{
    private readonly IWindow _window;
    private readonly Func<GL, IWindow, IOpenGLWindow> _createWindow;

    public OpenGLWindowHost(Func<GL, IWindow, IOpenGLWindow> createWindow, WindowOptions options)
    {
        _createWindow = createWindow;
        _window = Window.Create(options);
        _window.Load += Load;
    }

    public void Run()
    {
        _window.Run();
        _window.Dispose();
    }

    private void Load()
    {
        var gl = GL.GetApi(_window);
        var glWindow = _createWindow(gl, _window);

        glWindow.Load();
        _window.Update += glWindow.Update;
        _window.Render += glWindow.Render;
        _window.FramebufferResize += glWindow.FramebufferResize;
    }
}

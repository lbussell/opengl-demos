// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.Input.Glfw;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;

// This demo should display a plain black window.

// Needed for Native AOT. Usually Silk.NET does this automatically with
// reflection, but that doesn't work with Native AOT, so we have to do it
// manually.
GlfwWindowing.RegisterPlatform();
GlfwInput.RegisterPlatform();

var windowOptions = WindowOptions.Default;
var window = Window.Create(windowOptions);

window.Load += () =>
{
};

window.Update += (double deltaTime) =>
{
};

window.Render += (double deltaTime) =>
{
};

window.FramebufferResize += (Vector2D<int> newSize) =>
{
};

window.Run();
window.Dispose();

// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Silk.NET.Input.Glfw;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using Triangle;

// Needed for Native AOT. Usually Silk.NET does this automatically with
// reflection, but that doesn't work with Native AOT, so we have to do it
// manually.
GlfwWindowing.RegisterPlatform();
GlfwInput.RegisterPlatform();

var windowOptions = WindowOptions.Default;
var host = new OpenGLWindowHost(TriangleDemoWindow.Create, windowOptions);
host.Run();

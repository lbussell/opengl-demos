// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace Triangle;

internal static class Shaders
{
    public const string VertexShaderSource =
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

    public const string FragmentShaderSource =
        """
        #version 330 core

        out vec4 fragColor;

        void main()
        {
            // Output a solid orange color for now
            fragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        """;
}

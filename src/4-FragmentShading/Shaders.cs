// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace FragmentShading;

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

        uniform ivec2 u_resolution;
        // uniform float u_time;

        out vec4 fragColor;

        void main()
        {
            vec2 st = gl_FragCoord.xy / vec2(u_resolution);
            fragColor = vec4(st.x, st.y, 0.0f, 1.0f);
        }
        """;
}

#version 330 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aUVs;
layout (location = 2) in vec4 aCol;

out vec2 vUVs;
out vec4 vColor;

uniform mat4 projection;

void main()
{
  gl_Position = projection * vec4(aPos, 1.0f);
  vUVs = aUVs;
  vColor = aCol;
}
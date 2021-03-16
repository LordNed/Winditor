#version 330 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aUVs;

out vec2 vUVs;

uniform mat4 uProjMtx;

void main()
{
  gl_Position = uProjMtx * vec4(aPos, 1.0f);
  vUVs = aUVs;
}
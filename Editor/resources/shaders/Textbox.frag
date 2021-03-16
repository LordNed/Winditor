#version 330 core

uniform vec4 uSrcColor;
uniform vec4 uDstColor;

layout (binding = 0) sampler2D texture;

in vec2 vUVs;
in vec4 vColor;

out vec4 pixelColor;

void main()
{
  pixelColor = mix(uSrcColor, uDstColor, texture(texture, vUVs)) * vColor;
}
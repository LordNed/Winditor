#version 330 core

uniform sampler2D uTexture;

in vec2 vUVs;

out vec4 pixelColor;

void main()
{
  pixelColor = texture(uTexture, vUVs);
}
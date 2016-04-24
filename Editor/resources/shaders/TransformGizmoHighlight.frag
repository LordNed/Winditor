#version 130

// Input Data
in vec2 Tex0;

// Final Output
out vec4 PixelColor;

uniform sampler2D Texture;

void main()
{
	PixelColor = vec4(1, 1, 0, 1);
}
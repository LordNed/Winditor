#version 130
#version 130

// Input Data
in vec2 Tex0;
in vec4 Color0;

// Final Output
out vec4 PixelColor;

void main()
{
	PixelColor = vec4(Color0.rgb * 0.5 + 0.5, 1);
}
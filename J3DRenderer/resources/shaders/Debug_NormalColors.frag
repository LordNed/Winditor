#version 140

// Input Data
in vec4 Color0;

// Final Output
out vec4 PixelColor;

void main()
{
	vec3 normalColor = Color0.rgb * 0.5 + vec3(0.5, 0.5, 0.5);
	PixelColor = vec4(normalColor, 1);
}
#version 140

// Vertex Data Input
in vec3 RawPosition;

// Output
out vec4 Color0;

void main()
{
	// This is a screen-space shader
	gl_Position = vec4(RawPosition, 1);
	Color0 = vec4(1, 1, 1, 1);
}
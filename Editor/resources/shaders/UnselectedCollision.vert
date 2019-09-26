#version 130

// Vertex Data Input
in vec3 RawPosition;
in vec4 RawColor0;

// Output
out vec4 Color0;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;
uniform vec4 COLOR0_Amb;

void main()
{
	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
	Color0 = RawColor0;
}
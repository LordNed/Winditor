#version 130

// Vertex Data Input
in vec3 RawPosition;
in vec2 RawTex0;

// Output
out vec2 Tex0;
out vec4 ColorTint;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;
uniform vec4 COLOR0_Mat;

void main()
{
	Tex0 = RawTex0;
	ColorTint = COLOR0_Mat;

	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
}
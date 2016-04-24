#version 130

// Vertex Data Input
in vec3 RawPosition;
in vec2 RawTex0;

// Output
out vec2 Tex0;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;

void main()
{
	Tex0 = RawTex0;

	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
}
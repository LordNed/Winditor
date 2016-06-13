#version 140

// Vertex Data Input
in vec3 RawPosition;
in vec2 RawTex0;
in vec3 RawNormal;

// Output
out vec2 Tex0;
out vec4 colors_0;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;

uniform vec4 COLOR0_Amb;
uniform vec4 COLOR0_Mat;
uniform vec4 COLOR1_Mat;
uniform vec4 COLOR1_Amb;

void main()
{
	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);

	Tex0 = RawTex0;
	colors_0 = vec4(COLOR1_Amb.rgb, 1);
}
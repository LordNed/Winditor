#version 140

// Vertex Data Input
in vec3 RawPosition;
in vec2 RawTex0;

// Output
out vec2 Tex0;
out vec4 Color0;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;

struct GXLight
{
    vec4 Position;
    vec4 Direction;
    vec4 Color;
    vec4 CosAtten;
    vec4 DistAtten;
};

layout(std140) uniform LightBlock
{
	GXLight Lights[8];
};

void main()
{
	Tex0 = RawTex0;
	Color0 = Lights[0].Color;

	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
}
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

	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
	vec4 worldPos = ModelMtx * vec4(RawPosition, 1);

	vec4 lightAccum = vec4(0,0,0,0);
	vec3 ldir; float dist; float dist2; float attn;

	// LIGHT ZERO
	ldir = normalize(Lights[0].Position.xyz - worldPos.xyz);
	dist2 = dot(ldir, ldir);
	dist = sqrt(dist2);
	ldir = ldir/dist;
	attn = max(0.0, dot(ldir, Lights[0].Direction.xyz));
	attn = max(0.0, Lights[0].CosAtten.x + Lights[0].CosAtten.y*attn + Lights[0].CosAtten.z*attn*attn) / dot(Lights[0].DistAtten.xyz, vec3(1.0, dist, dist2));
	lightAccum.rgb += attn * max(0.0,dot(ldir, RawNormal)) * vec3(Lights[0].Color.rgb);

	// LIGHT ONE
	ldir = normalize(Lights[1].Position.xyz - worldPos.xyz);
	dist2 = dot(ldir, ldir);
	dist = sqrt(dist2);
	ldir = ldir/dist;
	attn = max(0.0, dot(ldir, Lights[1].Direction.xyz));
	attn = max(0.0, Lights[1].CosAtten.x + Lights[1].CosAtten.y*attn + Lights[1].CosAtten.z*attn*attn) / dot(Lights[1].DistAtten.xyz, vec3(1.0, dist, dist2));
	lightAccum.rgb += attn * max(0.0,dot(ldir, RawNormal)) * vec3(Lights[1].Color.rgb);

	vec4 illum = clamp(lightAccum, 0, 1);
	colors_0 = vec4(illum.rgb, 1);
}
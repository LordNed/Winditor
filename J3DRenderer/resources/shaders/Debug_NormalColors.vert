#version 140

// Vertex Data Input
in vec3 RawPosition;
in vec3 RawNormal;

// Output
out vec4 Color0;

uniform mat4 ModelMtx;
uniform mat4 ViewMtx;
uniform mat4 ProjMtx;


void main()
{
	Color0 = vec4(RawNormal.rgb, 1);

	mat4 MVP = ProjMtx * ViewMtx * ModelMtx;
	gl_Position = MVP * vec4(RawPosition, 1);
}
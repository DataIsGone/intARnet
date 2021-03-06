#ifndef __PSXEFFECTS_CGINC__
#define __PSXEFFECTS_CGINC__

sampler2D _MainTex;
sampler2D _Emission;
sampler2D _NormalMap;
sampler2D _SpecularMap;
sampler2D _MetalMap;
sampler2D _LODTex;
// sampler2D unity_Lightmap;
// float4 unity_LightmapST;
float4 _MainTex_ST;
fixed _VertexSnappingDetail;
fixed _VertexInaccuracy;
fixed _AffineMapping;
fixed _DrawDistance;
fixed _Specular;
float4 _Color;
fixed _DarkMax;
fixed _Unlit;
fixed _SkyboxLighting;
fixed _WorldSpace;
fixed _EmissionAmt;
fixed _Metallic;
fixed _Smoothness;
fixed _Triplanar;
fixed _DrawDist;
fixed _SpecModel;
fixed _DiffModel;
fixed _RenderMode;
fixed _CamPos;
fixed _LODAmt;
fixed _ShadowType;
samplerCUBE _Cube;

// This function snaps a vertex to a screen-space approximation
float4 PixelSnap(float4 pos)
{
	float2 hpc = _ScreenParams.xy * 0.75f;
	_VertexInaccuracy /= 8;
	float2 pixelPos = round((pos.xy / pos.w) * hpc / _VertexInaccuracy) * _VertexInaccuracy;
	pos.xy = pixelPos / hpc * pos.w;
	return pos;
}

float GetDither(float2 pos, float factor) {
	float DITHER_THRESHOLDS[16] =
	{
		1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
		13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
		4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
		16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
	};

	// Dynamic indexing isn't allowed in WebGL for some weird reason so here's this strange workaround
	#ifdef SHADER_API_GLES3
	int i = (int(pos.x) % 4) * 4 + int(pos.y) % 4;
	for (int x = 0; x < 16; x++)
		if (x == i)
			return factor - DITHER_THRESHOLDS[x];
	return 0;
	#else
	return factor - DITHER_THRESHOLDS[(uint(pos.x) % 4) * 4 + uint(pos.y) % 4];
	#endif
}

float2 PerformAffineMapping(float4 inp, float4 st, bool aff) {
	float2 rtn = inp.xy;
	if (aff)
		rtn = (inp / inp.z + st.zw) * st.xy;
	else
		rtn = (inp + st.zw) * st.xy;
	return rtn;
}

#endif
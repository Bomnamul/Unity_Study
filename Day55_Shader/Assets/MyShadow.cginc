#if !defined(MY_SHADOW_INCLUDED)
#define MY_SHADOW_INCLUDED

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION; // Vertex가 넘어올 때 위치값을 받음(자료구조에 대한 설명)
	float3 normal : NORMAL;
};

float4 vert (appdata v) : SV_POSITION
{
	float4 position = UnityClipSpaceShadowCasterPos(v.vertex.xyz, v.normal);
    return UnityApplyLinearShadowBias(position);
}

fixed4 frag (v2f i) : SV_Target
{
	return 0;
}

#endif

/*
|A|: A의 길이
dot(A, B) = |A||B| cos@

dot(A, A) = |A||A| cos@: cos0 = 1
		  = |A|^2
*/

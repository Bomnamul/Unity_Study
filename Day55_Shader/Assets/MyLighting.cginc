#if !defined(MY_LIGHTING_INCLUDED)
#define MY_LIGHTING_INCLUDED

#include "AutoLight.cginc"
#include "UnityPBSLighting.cginc"

struct appdata
{
    float4 vertex : POSITION; // Vertex가 넘어올 때 위치값을 받음(자료구조에 대한 설명)
    float2 uv : TEXCOORD0;
	float3 normal : NORMAL;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
	float3 normal : TEXCOORD1;
	float3 worldPos : TEXCOORD2;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float4 _Tint;
float _Metallic;
float _Smoothness;

v2f vert (appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
	o.worldPos = mul(unity_ObjectToWorld, v.vertex);
	o.normal = UnityObjectToWorldNormal(v.normal);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    return o;
}

UnityLight CreateLight(v2f i)
{
	UnityLight light;
	// light.dir = _WorldSpaceLightPos0.xyz;
	light.dir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
	// float3 lightVec = _WorldSpaceLightPos0.xyz - i.worldPos;
	// float attenuation = 1 / (1 + dot(lightVec, lightVec));
	UNITY_LIGHT_ATTENUATION(attenuation, 0, i.worldPos);
	light.color = attenuation * _LightColor0.rgb;;
	light.ndotl = DotClamped(i.normal, light.dir);
	return light;
}

fixed4 frag (v2f i) : SV_Target
{
	i.normal = normalize(i.normal); // Interpolator에서 어긋날 경우가 있기에 다시 nomalize
	// Directional lights: (World space dir, 0), Other lights: (World space pos, 1)
	// float3 lightDir = _WorldSpaceLightPos0.xyz; // Vector4(x, y, z, w)
	float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
	// float3 lightColor = _LightColor0.rgb;
	float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;

	float3 specularTint;
	float oneMinusReflectivity;
	albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

	UnityIndirect indirectLight;
	indirectLight.diffuse = 0;
	indirectLight.specular = 0;

    return UNITY_BRDF_PBS(
		albedo, specularTint, 
		oneMinusReflectivity, _Smoothness, 
		i.normal, viewDir, 
		CreateLight(i), indirectLight
	);
}

#endif

/*
|A|: A의 길이
dot(A, B) = |A||B| cos@

dot(A, A) = |A||A| cos@: cos0 = 1
		  = |A|^2
*/

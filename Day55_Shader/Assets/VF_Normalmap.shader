Shader "VF/VF_Normalmap"
{
    Properties
    {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
		[NoScaleOffset] _NormalMap ("Normal", 2D) = "bump" {}
		_BumpScale ("Bump Scale", Float) = 1
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }

	CGINCLUDE

	// #define BINORMAL_PER_FRAGMENT

	ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
			Tags {
				"LightMode"="ForwardBase" // _WorldSpaceLightPos0 and _LightColor0 사용 가능
			}

            CGPROGRAM
			#pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag

			#include "UnityPBSLighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION; // Vertex가 넘어올 때 위치값을 받음(자료구조에 대한 설명)
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;

				#if defined(BINORMAL_PER_FRAGMENT)
					float4 tangent : TEXCOORD3;
				#else
					float3 tangent : TEXCOORD3;
					float3 binormal : TEXCOORD4;
				#endif
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Tint;
			float _Metallic;
			float _Smoothness;

			sampler2D _NormalMap;
			float _BumpScale;

			float3 CreateBinormal(float3 normal, float3 tangent, float binormalSign)
			{
				return cross(normal, tangent) * (binormalSign * unity_WorldTransformParams.w);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				#if defined(BINORMAL_PER_FRAGMENT)
					o.tangent = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);
				#else
					o.tangent = UnityObjectToWorldDir(v.tangent.xyz);
					o.binormal = CreateBinormal(o.normal, o.tangent, v.tangent.w);
				#endif
				
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			void InitializeFragmentNormal(inout v2f i)
			{
				// i.normal.xy = tex2D(_NormalMap, i.uv).ag * 2 - 1;
				// i.normal.xy *= _BumpScale;
				// i.normal.z = sqrt(1 - saturate(dot(i.normal.xy, i.normal.xy)));
				float3 tangentSpaceNormal = UnpackScaleNormal(tex2D(_NormalMap, i.uv), _BumpScale);

				#if defined(BINORMAL_PER_FRAGMENT)
					float3 binormal = CreateBinormal(i.normal, i.tangent.xyz, i.tangent.w);
				#else
					float3 binormal = i.binormal;
				#endif

				i.normal = normalize(
					tangentSpaceNormal.x * i.tangent + 
					tangentSpaceNormal.y * binormal + 
					tangentSpaceNormal.z * i.normal
				); // (T B N) * (x, y, z)t

				// x^2 + y^2 + z^2 = 1^2
				// z^2 = 1 - x^2 - y^2
				// z = sqrt(1 - (x^2 + y^2)), x^2 + y^2 = dot(xy)
			}

            fixed4 frag (v2f i) : SV_Target
            {
				InitializeFragmentNormal(i);

				// Directional lights: (World space dir, 0), Other lights: (World space pos, 1)
				float3 lightDir = _WorldSpaceLightPos0.xyz; // Vector4(x, y, z, w)
				float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				float3 lightColor = _LightColor0.rgb;
				float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;
				//albedo *= tex2D(_HeightMap, i.uv);

				float3 specularTint;
				float oneMinusReflectivity;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

				UnityLight light;
				light.color = lightColor;
				light.dir = lightDir;
				light.ndotl = DotClamped(i.normal, lightDir);
				UnityIndirect indirectLight;
				indirectLight.diffuse = 0;
				indirectLight.specular = 0;

                return UNITY_BRDF_PBS(
					albedo, specularTint, 
					oneMinusReflectivity, _Smoothness, 
					i.normal, viewDir, 
					light, indirectLight
				);
            }
            ENDCG
        }
    }
}

Shader "VF/VF_GenerateNormals"
{
    Properties
    {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
		[NoScaleOffset] _HeightMap ("Heights", 2D) = "gray" {}
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
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

			sampler2D _HeightMap;
			float4 _HeightMap_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			void InitializeFragmentNormal(inout v2f i)
			{
				float2 du = float2(_HeightMap_TexelSize.x * 0.5, 0);
				float u1 = tex2D(_HeightMap, i.uv - du);
				float u2 = tex2D(_HeightMap, i.uv + du);
				// float3 tu = float3(1, u2 - u1, 0);

				float2 dv = float2(0, _HeightMap_TexelSize.y * 0.5);
				float v1 = tex2D(_HeightMap, i.uv - dv);
				float v2 = tex2D(_HeightMap, i.uv + dv);
				// float3 tv = float3(0, v2  - v1, 1);

				i.normal = float3(u1 - u2, 1, v1 - v2);
				i.normal = normalize(i.normal);
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

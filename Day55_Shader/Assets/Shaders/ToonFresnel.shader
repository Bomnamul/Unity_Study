Shader "Custom/ToonFresnel"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		cull back // Front side
		CGPROGRAM
        #pragma surface surf Toon noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingToon(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			if (ndotl > 0.7) { // 음영의 단계화
				ndotl = 1;
			} else {
				ndotl = 0.3;
			}

			float rim = abs(dot(s.Normal, viewDir));
			if (rim > 0.3) { // Outline
				rim = 1;
			} else {
				rim = -1; // ambient에 더해줘도 영향없을 정도로 작은 수(0을 넣어도 되긴 함)
			}

			float4 final;
			final.rgb = s.Albedo * ndotl * _LightColor0.rgb * rim;
			final.a = s.Alpha;

			return final;
		}

        ENDCG
    }
    FallBack "Diffuse"
}

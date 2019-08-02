Shader "Custom/CustomLambert"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf CustomLambert //noambient

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
            o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Alpha = c.a;
        }

		float4 LightingCustomLambert(SurfaceOutput s, float3 lightDir, float atten) {
			// return float4(1, 0, 0, 1);

			// Lambert
			// float ndotl = dot(s.Normal, lightDir); // cos(@)
			// float ndotl = saturate(dot(s.Normal, lightDir)); // 0 ~ 1까지 Clamp, max(0, cos(@))
			// return ndotl;

			// Half-Lambert by Valve
			// float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			// return pow(ndotl, 3);

			// Lambert full version: Legacy shader > Bumped Diffuse와 동일
			/*
			float ndotl = saturate(dot(s.Normal, lightDir)); // 0 ~ 1까지 Clamp, max(0, cos(@))
			float4 final;
			final.rgb = ndotl * s.Albedo * _LightColor0.rgb * atten; // _LightColor0: Directional light, atten: 거리에 따른 조명 감쇄효과(Point Light 사용 시 나타남)
			final.a = s.Alpha;
			return final;
			*/

			// Half-Lambert full version
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5; // 0 ~ 1까지 Clamp, max(0, cos(@))
			float4 final;
			final.rgb = pow(ndotl, 3) * s.Albedo * _LightColor0.rgb * atten; // _LightColor0: Directional light, atten: 거리에 따른 조명 감쇄효과(Point Light 사용 시 나타남)
			final.a = s.Alpha;
			return final;
		}
        ENDCG
    }
    FallBack "Diffuse"
}

// atten
// - Self shadow
// - Receive shadow
// - Attenuation(빛의 감쇄): Directional light는 영향이 없음(거리 개념이 없음), Point light는 영향 받음
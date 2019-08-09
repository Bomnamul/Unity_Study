Shader "Custom/ToonWarp"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_RampTex ("RampTex", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		CGPROGRAM
        #pragma surface surf Warp noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _RampTex;

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

		float4 LightingWarp(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
			// Outline
			float rim = abs(dot(s.Normal, viewDir));
			if (rim > 0.2) {
				rim = 1;
			} else {
				rim = -1;
			}

			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			float4 ramp = tex2D(_RampTex, float2(ndotl, 0.5));

			// Specular
			float3 specularColor;
			float3 H = normalize(lightDir + viewDir);
			float hdotn = saturate(dot(H, s.Normal));
			hdotn = pow(hdotn, 500); // Intensity
			// specularColor = hdotn * 1;
			specularColor = smoothstep(0.005, 0.01, hdotn) * 1; // * 1: Color값, flaot3(1, 1, 1)

			float4 final;
			final.rgb = s.Albedo * ramp.rgb * rim + specularColor;
			final.a = s.Alpha;

			return final;
		}

        ENDCG
    }
    FallBack "Diffuse"
}

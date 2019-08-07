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

		float4 LightingWarp(SurfaceOutput s, float3 lightDir, float atten) {
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			float4 ramp = tex2D(_RampTex, float2(ndotl, 0.5));

			return ramp;
		}

        ENDCG
    }
    FallBack "Diffuse"
}

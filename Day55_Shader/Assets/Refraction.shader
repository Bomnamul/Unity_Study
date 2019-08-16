Shader "Custom/Refraction"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		zwrite off

		GrabPass {}

        CGPROGRAM
        #pragma surface surf Nolight noambient alpha:fade

        sampler2D _MainTex;
		sampler2D _GrabTexture;

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos; // In clip space position
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 ref = tex2D (_MainTex, IN.uv_MainTex);
			float2 screenUV = IN.screenPos.rgb / IN.screenPos.a; // => 0 ~ 1 UV range, NDC(Normalized Device Coordinates)
			// o.Emission = IN.screenPos.rgb; // frac: 소숫점만 남김
			o.Emission = tex2D (_GrabTexture, screenUV.xy + ref.x * _SinTime.w * 0.05);
        }

		float4 LightingNolight(inout SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0, 0, 0, 1);
		}

        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/VertexLit" // No shadow
}

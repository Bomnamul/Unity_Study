Shader "Custom/2PassAlphaBlending"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

		// 1st Pass: Z-Buffer값을 갱신
		zwrite on // 기본으로 on이지만 명시
		ColorMask 0 // default: RGBA, 0: 아무것도 그리지 않음

        CGPROGRAM
        #pragma surface surf Nolight noambient noforwardadd nolightmap novertexlights noshadow

        struct Input
        {
            float4 color: Color; // Dummy
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
        }

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, 0);
		}

        ENDCG

		// 2nd Pass
		zwrite off
		// ZTest Less // defualt: LEqual, ZTest 통과(그리는) 조건: 거리가 같거나 작을경우 그린다.

		CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = 0.3;
        }
        ENDCG

    }
    FallBack "Diffuse"
}

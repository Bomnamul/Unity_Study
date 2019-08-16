Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_Cut ("Alpha Cut", Range(0, 1)) = 0.1
		[HDR] _OutColor ("Outline Color", Color) = (1, 1, 1, 1)
		_OutThickness ("Outline Thickness", Range(1, 1.5)) = 1.15
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
		sampler2D _NoiseTex;
		float _Cut;
		float4 _OutColor;
		float _OutThickness;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_NoiseTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 noise = tex2D (_NoiseTex, IN.uv_NoiseTex);
            o.Albedo = c.rgb;
			float alpha;
			if (noise.r >= _Cut) {
				alpha = 1;
			} else {
				alpha = 0;
			}
			float outline;
			if (noise.r >= _Cut * _OutThickness) {
				outline = 0;
			} else {
				outline = 1;
			}
			// o.Emission = float3(IN.uv_NoiseTex, 0); // MainTex의 uv와 같은 위치의 픽셀을 불러옴
			o.Emission = outline * _OutColor.rgb;
            o.Alpha = alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

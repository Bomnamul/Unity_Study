// https://docs.unity3d.com/kr/2018.1/Manual/SL-ShaderCompileTargets.html

Shader "Custom/FireEffect2"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" } // Queue(Render Queue)의 마지막에 그림?
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade // #pragma: 컴파일러 확장 지시자, Standard: 물리 기반 렌더링(PBR, 무거움)

        sampler2D _MainTex;
		sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex; // 4 byte 짜리 float
			float2 uv_MainTex2;
        };

        // half _Glossiness; // 16 bit (2 byte)
        // half _Metallic;
        // fixed4 _Color; // half의 절반 (1 byte)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y - _Time.y));
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + d.r); // 2번째 tex의 각 픽셀값만큼 uv에 더함
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

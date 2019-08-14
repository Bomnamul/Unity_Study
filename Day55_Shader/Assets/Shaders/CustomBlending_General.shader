Shader "Custom/CustomBlending_General"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("SrcBlend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("DstBlend Mode", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"} // Opaque보다 뒤에 그린다는 의미 (Queue의 Transparent지점에 넣음)
		zwrite off
		Blend [_SrcBlend] [_DstBlend]

		// (A) * Source + (B) * Destination
		// Source: Alpha object
		// Destination(=Back buffer)

        CGPROGRAM
        #pragma surface surf Lambert keepalpha

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/VertexLit" // No shadow
}

/*
* 정리
* 1. Opaque objects를 먼저 그림: Z-Buffering
* 2. Transparent objects를 뒤에서부터 그린다: Alpha sorting
* 3. Transparent objects는 ZWrite off 하고 그린다(Z-Buffer 기능을 꺼버림)
*/
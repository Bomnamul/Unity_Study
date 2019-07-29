Shader "Custom/VertexColor"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
		_MainTex3 ("Albedo (RGB)", 2D) = "white" {}
		_MainTex4 ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float2 uv_MainTex4;

			float4 color:Color; // Vertex마다 Color 값을 받아옴
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);
			fixed4 e = tex2D (_MainTex3, IN.uv_MainTex3);
			fixed4 f = tex2D (_MainTex4, IN.uv_MainTex4);

			float3 lerp1 = lerp(c, d, IN.color.r);
			float3 lerp2 = lerp(lerp1, e, IN.color.g);
			float3 lerp3 = lerp(lerp2, f, IN.color.b);
			o.Albedo = lerp3.rgb;

            // o.Albedo = IN.color;
            // o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

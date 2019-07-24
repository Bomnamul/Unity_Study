Shader "Lecture/TextureMixTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MainTex2 ("Texture", 2D) = "white" {}
		_Blending ("Blending", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
		sampler2D _MainTex2;
		float _Blending;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);
			// o.Albedo = lerp(c, d, _Blending);
			// o.Albedo = lerp(c.rgb, d.rgb, _Blending);
			o.Albedo = lerp(c.rgb, d.rgb, c.a);
			o.Albedo = lerp(o.Albedo, d.rgb, _Blending);
            // o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

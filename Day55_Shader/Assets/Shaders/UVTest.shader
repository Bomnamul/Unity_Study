Shader "Lecture/UVTest"
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

		// float4 _Time(t / 20, t, 2 * t, 3 * t);

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex * 2);
			fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2 + _Time.x);
			// o.Albedo = float3(IN.uv_MainTex.x, IN.uv_MainTex.y, 0);
			o.Albedo = d.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

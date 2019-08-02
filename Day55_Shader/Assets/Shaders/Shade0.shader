Shader "Lecture/Shade0"
{
    Properties
    {
        _Range_R ("Range R", Range(0,1)) = 0.0
		_Range_G ("Range G", Range(0,1)) = 0.0
		_Range_B ("Range B", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _Range_R;
		float _Range_G;
		float _Range_B;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            // fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // o.Albedo = c.rgb;

			float3 r3 = float3(1, 0, 0);
			float3 g3 = float3(0, 1, 0);
			float3 b3 = float3(0, 0, 1);
			float g1 = 0.5f;

			// o.Emission = 1 - r3; // 보색
			// o.Emission = float3(r3.r, g3.g, b3.b);
			o.Emission = float3(_Range_R, _Range_G, _Range_B);
            // Metallic and smoothness come from slider variables
            // o.Metallic = _Metallic;
            // o.Smoothness = _Glossiness;
            // o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

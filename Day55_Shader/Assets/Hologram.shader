Shader "Custom/Hologram"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transaprent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Lambert noambient alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir; // From vertex to camera, lightDir: From vertex to light
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            // o.Albedo = c.rgb;

			o.Emission = float3(0, 0.5, 1);
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1 - rim, 3);

            // o.Alpha = rim * sin(_Time.y * 3);
			// o.Alpha = rim * abs(sin(_Time.y * 3));
			o.Alpha = rim * (sin(_Time.y * 3) * 0.5 + 0.5);
        }
        ENDCG
    }
    FallBack "Diffuse"
}

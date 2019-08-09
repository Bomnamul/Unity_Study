Shader "Custom/NormalTest"
{
    Properties
    {
		_BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert noambient

		sampler2D _BumpMap;

        struct Input
        {
			float2 uv_BumpMap;
			float3 worldNormal;
			INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
			// In tangent space
			// o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			// o.Emission = o.Normal;

			// In world space (Vertex normal)
			// o.Emission = IN.worldNormal;

			// In world space with NormalMap;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			float3 worldNormal = WorldNormalVector(IN, o.Normal);
			o.Emission = worldNormal;

            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

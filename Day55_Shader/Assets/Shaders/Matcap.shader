Shader "Custom/Matcap"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("NormalMap", 2D) = "bump" {}
		_Matcap ("Matcap", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Nolight noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _Matcap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 worldNormal;
			INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			float3 worldN = WorldNormalVector(IN, o.Normal);
			float3 viewNormal = mul((float3x3)UNITY_MATRIX_V, worldN); // World 기준의 Vector를 Camera 기준으로 변경 (Unity의 Camera는 오른손 좌표계 사용)
			float2 matcapUV = viewNormal * 0.5 + 0.5f;
			// o.Albedo = c.rgb;
			// o.Emission = float3(matcapUV, 0);
			o.Emission = tex2D(_Matcap, matcapUV) * c.rgb; // Matcap: 빛의 강도를 결정
            o.Alpha = c.a;
        }

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, s.Alpha);
		}

        ENDCG
    }
    FallBack "Diffuse"
}

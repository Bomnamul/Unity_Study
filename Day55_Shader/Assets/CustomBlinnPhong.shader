Shader "Custom/CustomBlinnPhong"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_GlossTex ("Gloss Tex", 2D) = "white" {}
		_SpecCol ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecPow ("Specular Power", Range(10, 200)) = 100
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf CustomBlinnPhong noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _GlossTex;
		float4 _SpecCol;
		float _SpecPow;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_GlossTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D (_GlossTex, IN.uv_GlossTex);
            o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Gloss = m.a;
            o.Alpha = c.a;
        }

		float4 LightingCustomBlinnPhong(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
			float4 final;

			// Lambert term: Diffuse
			float3 diffuseColor;
			float ndotl = saturate(dot(s.Normal, lightDir)); // 0 ~ 1까지 Clamp, max(0, cos(@))
			diffuseColor = ndotl * s.Albedo * _LightColor0.rgb * atten;

			// Specular term
			float3 specularColor;
			float3 H = normalize(lightDir + viewDir);
			float spec = saturate(dot(H, s.Normal));
			// spec = pow(spec, 100);
			spec = pow(spec, _SpecPow);
			specularColor = spec * _SpecCol.rgb * s.Gloss;
			final.rgb = diffuseColor.rgb + specularColor.rgb;
			final.a = s.Alpha;

			return final;
		}
        ENDCG
    }
    FallBack "Diffuse"
}

// atten
// - Self shadow
// - Receive shadow
// - Attenuation(빛의 감쇄): Directional light는 영향이 없음(거리 개념이 없음), Point light는 영향 받음
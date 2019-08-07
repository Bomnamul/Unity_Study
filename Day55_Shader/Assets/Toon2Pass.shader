Shader "Custom/Toon2Pass"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		cull front
		// 1st Pass
        CGPROGRAM
        #pragma surface surf Nolight vertex:vert noambient noshadow // addshadow: 바뀐 vertex에 대해 새로 shadow를 그림

        sampler2D _MainTex;

		void vert(inout appdata_full v) {
			//v.vertex.xyz += v.normal.xyz * 0.01 * sin(_Time.y);
			v.vertex.xyz += v.normal.xyz * 0.002;
		}

        struct Input
        {
            float4 color:COLOR; // Dummy
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            
        }

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, 1);
		}

        ENDCG

		cull back
		// 2nd Pass
		CGPROGRAM
        #pragma surface surf Toon noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten) {
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			if (ndotl > 0.7) {
				ndotl = 1;
			} else {
				ndotl = 0.3;
			}

			/*
			ndotl = ndotl * 5;
			ndotl = ceil(ndotl) / 5; // 0.2, 0.4 ..., 소숫점 자리가 있으면 올림, 반대는 floor?
			*/

			float4 final;
			final.rgb = s.Albedo * ndotl * _LightColor0.rgb;
			final.a = s.Alpha;

			return final;
		}

        ENDCG
    }
    FallBack "Diffuse"
}

Shader "VF/TexturedWithDetail"
{
    Properties
    {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
		_DetailTex ("Detail Texture", 2D) = "gray" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float2 uvDetail : TEXCOORD1;
            };

            sampler2D _MainTex, _DetailTex;
            float4 _MainTex_ST, _DetailTex_ST;
			float4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvDetail = TRANSFORM_TEX(v.uv, _DetailTex); // TRANSFORM_TEX: Tiling, Offset
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv) * _Tint;
				color *= tex2D(_DetailTex, i.uvDetail) * unity_ColorSpaceDouble;
				// Gamma Space: 2, Linear Space: 4.59 (Color값을 여러번 곱해야 할 경우 unity_ColorSpaceDouble를 사용해야 동일한 결과를 얻는다)
                return color;
            }
            ENDCG
        }
    }
}

/*
	Texture file(sRGB)	->		Shader		->		 Rendering(Display)

		Gamma					Gamma						Gamma
								0.5 * 2 = 1

		Gamma					Linear						Gamma		(현재 우리가 사용하는 방식)
								0.5^2.2 = 0.217
								0.217 * 2 = 0.44
								=> 0.217 * a = 1
								a = 4.59
								0.5^2.2 * (1 / (0.5^2.2)) => 1
*/
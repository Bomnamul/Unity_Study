Shader "VF/VF_AlphaBlending"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
		{ 
			"RenderType"="Transparent" 
			"Queue"="Transparent" 
			"PreviewType"="Plane" 
		}

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha
			zwrite off

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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed lum = col.r * 0.3 + col.g * 0.59 + col.b * 0.11;
				fixed4 grayscale = fixed4(lum, lum, lum, col.a);
                return grayscale;
            }
            ENDCG
        }
    }
}

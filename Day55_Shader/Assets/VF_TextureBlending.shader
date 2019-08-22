Shader "VF/VF_TextureBlending"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("Second Texture", 2D) = "white" {}
		_Tween ("Tween", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		zwrite off

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

            struct v2f // Vertex를 가지고 Interpolation하여 fragment를 채움: Interpolator
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _SecondTex;
            float4 _MainTex_ST;
			fixed _Tween;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
				fixed4 color2 = tex2D(_SecondTex, i.uv);
				fixed4 c = lerp(color, color2, _Tween);
                return c;
            }
            ENDCG
        }
    }
}

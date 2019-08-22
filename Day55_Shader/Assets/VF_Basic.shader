Shader "VF/VF_Basic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "PreviewType"="Plane" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag // Pixel Shader라고 보면 됨
            
            #include "UnityCG.cginc" // Macro가 정의되어 있음

            struct appdata // struct(구조체): Call by Value, class: Call by Reference
            {
                float4 vertex : POSITION; // : POSITION: Context로 이해
                float2 uv : TEXCOORD0;
            };

            struct v2f // Vertex to Fragment
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST; // ST: Scale and Translation => TO(Tiling and Offset), S: Tiling, T: Offset

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);				// Local에서 정의된 Vertex정보를 Clip space(Projection space)로 정의해줌
				// or o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);		// MVP: Model(World) * View(Camera) * Projection(Perspective)
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);					// 대문자로 구성된 함수: Macro(컴파일되기 전에 전처리기가 매크로를 코드로 교체해버림)
				// or o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target // SV_Target: Frame buffer
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}

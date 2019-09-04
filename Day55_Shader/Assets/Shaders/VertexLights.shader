Shader "VF/Vertexlights"
{
    Properties
    {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
			Tags {
				"LightMode"="ForwardBase"
			}

            CGPROGRAM
			#pragma target 3.0

            #pragma multi_compile _ VERTEXLIGHT_ON

            #pragma vertex vert
            #pragma fragment frag

			#include "MyVertexLighting.cginc"

            ENDCG
        }

		Pass
        {
			Tags {
				"LightMode"="ForwardAdd"
			}
			Blend One One // ForwardBase를 먼저 그리고 그 뒤에 Additive
			zwrite off

            CGPROGRAM
			#pragma target 3.0

            // #pragma multi_compile DIRECTIONAL DIRECTIONAL_COOKIE POINT POINT_COOKIE SPOT
            #pragma multi_compile_fwdadd

            #pragma vertex vert
            #pragma fragment frag

			// #define POINT

			#include "MyVertexLighting.cginc"

            ENDCG
        }
    }
}

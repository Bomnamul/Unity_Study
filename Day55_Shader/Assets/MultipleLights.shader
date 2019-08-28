Shader "VF/MultipleLights"
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

            #pragma vertex vert
            #pragma fragment frag

			#include "MyLighting.cginc"

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

            #pragma vertex vert
            #pragma fragment frag

			#define POINT

			#include "MyLighting.cginc"

            ENDCG
        }
    }
}

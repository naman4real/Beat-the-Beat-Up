Shader "Custom/highlightVertex"
{
    Properties{
         _Color("Color", Color) = (1,1,1,1)
         _MainTex("Albedo (RGB)", 2D) = "white" {}
         _Glossiness("Smoothness", Range(0,1)) = 0.5
         _Metallic("Metallic", Range(0,1)) = 0.0
         _PartMap("Part Map", 2D) = "white" {}
    }
    SubShader{
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert fullforwardshadows
        #pragma target 3.0
        struct Input {
            float2 uv_MainTex;
            float3 vertexColor; // Vertex color stored here by vert() method
        };

        struct v2f {
            float4 pos : SV_POSITION;
            fixed4 color : COLOR;
        };


        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.vertexColor = v.color; // Save the Vertex Color in the Input for the surf() method
        }

        sampler2D _MainTex;
        sampler2D _PartMap;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        int _PartIndex;
        /*-------------------
        0 : right - stomach
        1 : mid - stomach
        2 : left - stomach
        3 : chest
        4 : left - arm
        5 : left - hand
        6 : right - head
        7 : mid - head
        8 : left - head
        9 : right - arm
        10 : right - hand
        11 : default
        -------------------*/

        int GetPartFromIndex(fixed4 partInfo)
        {
            if (partInfo.b == 0)    // b=0: top (clothes)
            {
                if (partInfo.r == 0)    // r=0: right stomach 
                    return 0;
                else if (partInfo.r - 0.5 < 0.1) // r=0.5: chest or mid stomach
                {
                    if (partInfo.g == 1)   // g=1: mid stomach
                        return 1;
                    if (partInfo.g - 0.5 < 0.1)  // g=0.5: chest
                        return 3;
                }
                else if (partInfo.r == 1)   // r=1: left stomach
                    return 2;
            }
            else if (partInfo.b - 0.5 < 0.1) // b=0.5: body
            {
                if (partInfo.g == 0) // g=0: head
                {
                    if (partInfo.r == 0)
                        return 6;   // right head
                    if (partInfo.r == 1)
                        return 8;   // left head
                    if (partInfo.r - 0.5 < 0.1)
                        return 7;   // mid head
                }
                else if (partInfo.r == 0) //r=0: right arm/hand
                {
                    if (partInfo.g == 1)
                        return 10;   // right hand
                    if (partInfo.g - 0.5 < 0.1)
                        return 9;   // right arm
                }
                else if (partInfo.r == 1)
                {
                    if (partInfo.g == 1)
                        return 5;  // left hand
                    if (partInfo.g - 0.5 < 0.1)
                        return 4;   // left arm
                }
            }
            // else default
            return 11;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 partInfo = tex2D(_PartMap, IN.uv_MainTex);
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // if vertex index matches the one to highlight
            if (_PartIndex == GetPartFromIndex(partInfo))
                c *= float4(1, 0, 0, 1);

            o.Albedo = c.rgb * IN.vertexColor; // Combine normal color with the vertex color
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        }
        FallBack "Diffuse"
}

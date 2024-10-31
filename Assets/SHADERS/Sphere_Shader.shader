Shader "Custom/PulsingEmissionInverted"
{
    Properties
    {
        _Color ("Base Color", Color) = (1, 1, 1, 1)
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionStrength ("Emission Strength", Range(0, 10)) = 1.0
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        // Inverted normals by rendering backfaces instead of frontfaces
        Cull Front  // This renders the backfaces, making the inside of the sphere visible

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
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _EmissionColor;
            float _EmissionStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Base texture color
                half4 texCol = tex2D(_MainTex, i.uv) * _Color;

                // Emission color based on audio-driven emission strength
                half4 emission = _EmissionColor * _EmissionStrength;

                // Return final color with emission
                return texCol + emission;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}



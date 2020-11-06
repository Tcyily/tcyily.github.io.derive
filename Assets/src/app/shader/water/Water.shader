// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Water"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" 
        _WaveTex ("Wave Texture", 2D) = "white"
        _MirrorTex ("Mirror Texture", 2D) = "white"
    }
    SubShader
    {
        Tags
        { 
            "RenderType" = "Transparent"
            "RenderType" = "water_effect_"
        }
        pass{
            Tags{"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "Lighting.cginc"

            sampler2D _MainTex;
            sampler2D _WaveTex;
            sampler2D _MirrorTex;
            fixed4 _Color;
            float4 _MainTex_ST;
            float4 _WaveTex_ST;
            float4 _MirrorTex_ST;

            struct a2v{
                float4 position : POSITION;
                float3 normal : NORMAL;
                float4 uv_main : TEXCOORD0;
                float4 uv_wave : TEXCOORD1;
                float4 uv_mirror : TEXCOORD2;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float3 world_normal : TEXCOORD0;
                float2 uv_main : TEXCOORD1;
                float2 uv_wave : TEXCOORD2;
                float2 uv_mirror : TEXCOORD3;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.position);
                o.world_normal = UnityObjectToWorldNormal(v.normal);
                o.uv_main = TRANSFORM_TEX(v.uv_main, _MainTex);
                o.uv_wave = TRANSFORM_TEX(v.uv_wave, _WaveTex);
                o.uv_mirror = TRANSFORM_TEX(v.uv_mirror, _MirrorTex);
                return o;
            }
            fixed4 frag(v2f i) : SV_Target{
                return fixed4(0.0f, 0.0f, 0.0f, 1.0f);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

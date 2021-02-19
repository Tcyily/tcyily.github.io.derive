// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "WaterEffect/MirrorEffect"
{
    Properties
    {
        
        _fresnelBase("fresnel Base", Range(0, 1)) = 1
        _fresnelScale("fresnel Scale", Range(0, 1)) = 1

        _fresnelIndensity("fresnel Indensity", Range(0, 5)) = 5
        _ReflectFactor("Reflect RGBA Factor", Color) = (1,1,1,1)
        _RefractFactor("Refract RGBA Factor", Color) = (1,1,1,1)
        _WaveTex ("Wave Texture", 2D) = "white"
        _ReflectTex ("Mirror Texture", 2D) = "white"
        _RefractTex("Refract Texture", 2D) = "white"
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

            sampler2D _WaveTex;
            sampler2D _ReflectTex;
            sampler2D _RefractTex;
            half4 _WaveTex_ST;

            fixed4 _ReflectFactor;
            fixed4 _RefractFactor;
            float _fresnelBase;
            float _fresnelScale;
            float _fresnelIndensity;

            struct a2v{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv_wave : TEXCOORD0;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float3 world_normal : TEXCOORD0;
                float2 uv_wave : TEXCOORD1;
                float4 pos_screen: TexCoord3;
                float3 dir_world_view: Texcoord4;
            };

            float Sinusoid(float A, float2 D, float4 pos, float w, float t, float offset){
                return A * sin(dot(D.x * pos.x, D.y * pos.z) * w + t * offset);
            }

            v2f vert(a2v v){
                v2f o;
                //common

                o.world_normal = normalize(UnityObjectToWorldNormal(v.normal));

                //水波效果施工中
                o.uv_wave = TRANSFORM_TEX(v.uv_wave, _WaveTex);
                v.vertex.y += Sinusoid(0.5f, float2(1.0f, 1.0f), v.vertex, 1.3376923f, _Time.y, 0.03125f);

                //用于镜面反射(mirror)
                o.pos = UnityObjectToClipPos(v.vertex);
                o.pos_screen = ComputeScreenPos(o.pos);

                //用于菲涅尔效果(fresnel)
                o.dir_world_view = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                half4 reflection_color = tex2D(_ReflectTex, i.pos_screen.xy/i.pos_screen.w);
                half4 refraction_color = tex2D(_RefractTex, i.pos_screen.xy/i.pos_screen.w);
                float fresnel_factor = _fresnelBase + _fresnelScale * pow(1 - dot(i.world_normal, i.dir_world_view), _fresnelIndensity);//base + scale* (1-dot(n,v))^indensity

                return tex2D(_WaveTex, i.uv_wave);
                //return fixed4(i.pos.y/i.pos.w, i.pos.y/i.pos.w, i.pos.y/i.pos.w, 1.0f);
                return lerp(refraction_color*_RefractFactor*2, reflection_color*_ReflectFactor*2, fresnel_factor);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

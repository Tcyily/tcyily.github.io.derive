Shader "Custom/FunnyBoxShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _AlphaScale ("Alpha", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"} 
        pass{
            Tags {"LightMode"="ForwardBase"}
            Cull Front
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            sampler2D _MainTex;
            half4 _MainTex_ST;
            float4 _Color;
            float _AlphaScale;

            struct a2v{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
            fixed4 frag(v2f i):SV_Target{
                float3 worldNormal = normalize(i.worldNormal);
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                
                float4 texColor = tex2D(_MainTex, i.uv);
                float4 albedo = texColor * _Color;
                float3 ambientColor = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo.rgb; 
                float3 diffuseColor = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
                return fixed4(ambientColor+diffuseColor,_AlphaScale);
            }
            ENDCG
        }

         pass{
            Tags {"LightMode"="ForwardBase"}
            Cull Back
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            sampler2D _MainTex;
            half4 _MainTex_ST;
            float4 _Color;
            float _AlphaScale;

            struct a2v{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
            fixed4 frag(v2f i):SV_Target{
                float3 worldNormal = normalize(i.worldNormal);
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                
                float4 texColor = tex2D(_MainTex, i.uv);
                float4 albedo = texColor * _Color;
                float3 ambientColor = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo.rgb; 
                float3 diffuseColor = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
                return fixed4(ambientColor+diffuseColor,_AlphaScale);
            }
            ENDCG
        }

    }
    FallBack "Diffuse"
}

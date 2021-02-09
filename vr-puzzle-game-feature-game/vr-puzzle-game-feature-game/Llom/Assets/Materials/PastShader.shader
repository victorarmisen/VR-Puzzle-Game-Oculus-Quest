﻿Shader "Custom/PastShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
        // Ambient light is applied uniformly to all surfaces on the object.
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        // Controls the size of the specular reflection.
        _Glossiness("Glossiness", Float) = 32
    }
    SubShader
    {
        Pass
        {
            // Setup our pass to use Forward rendering, and only receive
            // data on the main directional light and ambient light.
            Tags
            {
                "RenderType"="Transparent" 
                "Queue"="Transparent"
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }
            //Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Compile multiple versions of this shader depending on lighting settings.
            #pragma multi_compile_fwdbase
            
            #include "UnityCG.cginc"
            // Files below include macros and functions to assist
            // with lighting and shadows.
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;				
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;	
                // Macro found in Autolight.cginc. Declares a vector4
                // into the TEXCOORD2 semantic with varying precision 
                // depending on platform target.
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);		
                o.viewDir = WorldSpaceViewDir(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // Defined in Autolight.cginc. Assigns the above shadow coordinate
                // by transforming the vertex from world space to shadow-map space.
                TRANSFER_SHADOW(o)
                return o;
            }
            
            float4 _Color;

            float4 _AmbientColor;

            float4 _SpecularColor;
            float _Glossiness;

            float4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);

                // Lighting below is calculated using Blinn-Phong,
                // with values thresholded to creat the "toon" look.
                // https://en.wikipedia.org/wiki/Blinn-Phong_shading_model

                // Calculate illumination from directional light.
                // _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
                // direction of the main directional light.
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                // Samples the shadow map, returning a value in the 0...1 range,
                // where 0 is in the shadow, and 1 is not.
                float shadow = SHADOW_ATTENUATION(i);
                // Partition the intensity into light and dark, smoothly interpolated
                // between the two to avoid a jagged break.
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);	
                // Multiply by the main directional light's intensity and color.
                float4 light = lightIntensity * _LightColor0;

                // Calculate specular reflection.
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                // Multiply _Glossiness by itself to allow artist to use smaller
                // glossiness values in the inspector.
                /*float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensity * _SpecularColor;		*/
                float4 specular = pow(saturate(NdotH), _Glossiness) * _SpecularColor;

                float4 sample = tex2D(_MainTex, i.uv);

                //float4 color = (light + _AmbientColor + specular) * _Color * sample;
                float4 color = (light + specular) * _Color * sample;
                
                color.a = lightIntensity > 0.1 ? 0 : 1;

                if (lightIntensity > 0) discard;

                return color;
            }
            ENDCG
        }

        // Shadow casting support.
        UsePass "Standard/ShadowCaster"
    }
}
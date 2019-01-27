﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Sofa"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _expansionMax ("Max Expansion",Float) = 1.0
        _maxDistance ("Max Distance",Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;


        struct Input
        {
            float2 uv_MainTex;
            float4 color : COLOR;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float3 _handPosition;
        float3 _gapDirection = float3(1,0,0);
		float _handDepth;
		float _expansionMax;
        float _maxDistance;

        static const float oneSixthSqrt = 0.408248;

        void vert (inout appdata_full v) {
            
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
            float3 expansionRatio = 0.5*_gapDirection + float3(oneSixthSqrt,oneSixthSqrt,oneSixthSqrt);
			float expansion = max(0, _maxDistance-length(worldPos-_handPosition) ) / _maxDistance ;
            
            v.color *= 1-sqrt( expansion );
			v.vertex.xyz -= v.normal * expansion * expansion * expansion * _expansionMax ;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * IN.color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

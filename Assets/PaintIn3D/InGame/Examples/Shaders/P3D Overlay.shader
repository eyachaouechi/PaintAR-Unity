Shader "Paint in 3D/P3D Overlay"
{
	Properties
	{
		[NoScaleOffset]_MainTex("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_MainTex_Transform("Scale (XY) Offset (ZW)", Vector) = (1.0, 1.0, 0.0, 0.0)
		[NoScaleOffset][Normal]_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpMap_Transform("Scale (XY) Offset (ZW)", Vector) = (1.0, 1.0, 0.0, 0.0)
		[NoScaleOffset]_MetallicGlossMap("Metallic (R) Ambient Occlusion (G) Smoothness(B)", 2D) = "white" {}
		_MetallicGlossMap_Transform("Scale (XY) Offset (ZW)", Vector) = (1.0, 1.0, 0.0, 0.0)
		[NoScaleOffset]_EmissionMap("Emission (RGB)", 2D) = "white" {}
		_EmissionMap_Transform("Scale (XY) Offset (ZW)", Vector) = (1.0, 1.0, 0.0, 0.0)

		_Color("Color", Color) = (1,1,1,1)
		_Coord("Coord (UV0, UV1, UV2, UV3)", Vector) = (1.0, 0.0, 0.0, 0.0)
		_BumpScale("Normal Map Strength", Range(0,5)) = 1
		_Metallic("Metallic", Range(0,1)) = 0
		_GlossMapScale("Smoothness", Range(0,1)) = 1
		_Emission("Emission", Color) = (0,0,0)
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha
		Tags{ "RenderType"="Transparent" "Queue"="AlphaTest" }
		ZWrite Off
		LOD 400
		CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert keepalpha
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityPBSLighting.cginc"

			#define P3D_DECLARE(NAME) sampler2D NAME; float4 NAME##_Transform
			#define P3D_SAMPLE(NAME, UV) tex2D( NAME , ( (UV) * NAME##_Transform.xy + NAME##_Transform.zw ) )

			P3D_DECLARE(_MainTex);
			P3D_DECLARE(_BumpMap);
			P3D_DECLARE(_MetallicGlossMap);
			P3D_DECLARE(_EmissionMap);

			float4 _Color;
			float4 _Coord;
			float  _BumpScale;
			float  _Metallic;
			float  _GlossMapScale;
			float3 _Emission;

			struct Input
			{
				float2 coord;
				float3 viewDir;
			};

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);

				o.coord = v.texcoord.xy * _Coord.x + v.texcoord1.xy * _Coord.y + v.texcoord2.xy * _Coord.z + v.texcoord3.xy * _Coord.w;
			}

			void surf(Input i, inout SurfaceOutputStandard o)
			{
				float4 texMain = P3D_SAMPLE(_MainTex, i.coord);
				float4 gloss   = P3D_SAMPLE(_MetallicGlossMap, i.coord);
				float4 bump    = P3D_SAMPLE(_BumpMap, i.coord);
				float4 glow    = P3D_SAMPLE(_EmissionMap, i.coord);

				o.Albedo     = texMain.rgb * _Color.rgb;
				o.Normal     = UnpackScaleNormal(bump, _BumpScale);
				o.Metallic   = gloss.r * _Metallic;
				o.Occlusion  = gloss.g;
				o.Smoothness = gloss.b * _GlossMapScale;
				o.Emission   = glow.rgb * _Emission;
				o.Alpha      = texMain.a * _Color.a;
			}
		ENDCG
	}
	FallBack "Standard"
}
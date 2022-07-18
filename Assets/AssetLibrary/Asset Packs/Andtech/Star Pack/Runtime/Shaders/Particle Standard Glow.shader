
Shader "Andtech/Star Pack/Particle Standard Glow" {

	Properties{
		// Main Options
		[HDR] [Gamma] _Color("_Color", Color) = (1.0, 0.0, 0.0, 1.0)
		[NoScaleOffset]
		_MainTex("Glow Mask", 2D) = "white" {}
		_SoftParticlesNearFadeDistance("_SoftParticlesNearFadeDistance", Float) = 0.0
		_SoftParticlesFarFadeDistance("_SoftParticlesFarFadeDistance", Float) = 1.0
		_CameraNearFadeDistance("_CameraNearFadeDistance", Float) = 1.0
		_CameraFarFadeDistance("_CameraFarFadeDistance", Float) = 2.0
		_Brightness("Brightness", Range(0.0, 1.0)) = 1.0
		// Twinkle Options
		_TwinkleAmount("Twinkle Amount", Range(0.0, 1.0)) = 0.8
		_TwinkleSpeed("Twinkle Speed", Range(1.0, 10.0)) = 5.0
		// Internal
		[HideInInspector]
		[Toggle] _SoftParticlesEnabled("_SoftParticlesEnabled", Float) = 0.0
		[HideInInspector]
		_SoftParticleFadeParams("_SoftParticleFadeParams", Vector) = (0.0, 0.0, 0.0, 0.0)
		[HideInInspector]
		[Toggle] _CameraFadingEnabled("_CameraFadingEnabled", Float) = 0.0
		[HideInInspector]
		_CameraFadeParams("_CameraFadeParams", Vector) = (0.0, 0.0, 0.0, 0.0)
		[HideInInspector]
		[Toggle] _TwinkleEnabled("_TwinkleEnabled", Float) = 0.0
	}

	SubShader {
		Blend SrcAlpha One
		Cull Off
		ZWrite Off
		Lighting Off
		ColorMask RGB

		Tags {
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"LightMode" = "Always"
			"PassFlags" = "OnlyDirectional"
			"PreviewType" = "Plane"
		}

		LOD 200

		Pass {
			CGPROGRAM
			#pragma target 3.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile_instancing
			#pragma multi_compile_particles
			#pragma multi_compile SOFTPARTICLES_ON
			#pragma shader_feature_local _FADING_ON
			#pragma shader_feature _TWINKLE_ON

			#include "UnityCG.cginc"
			#include "UnityInstancing.cginc"

			struct appdata {
				float4 vertex : POSITION;
				half4 color : COLOR;
				float3 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half4 color : COLOR;
				UNITY_FOG_COORDS(0)
#if defined(_FADING_ON)
					float4 projectedPosition : TEXCOORD1;
#endif
				float2 uv : TEXCOORD2;
			};

			sampler2D _MainTex;
			fixed _Brightness;
			fixed _TwinkleAmount;
			fixed _TwinkleSpeed;

			float4 _SoftParticleFadeParams;
			float4 _CameraFadeParams;
			float _ResizeAmount;

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
			UNITY_INSTANCING_BUFFER_END(Props)
			UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

			static const half4 COLOR_CLEAR = half4(0.0, 0.0, 0.0, 0.0);

			#define UNITY_MATRIX_M unity_ObjectToWorld
			#define SOFT_PARTICLE_NEAR_FADE _SoftParticleFadeParams.x
			#define SOFT_PARTICLE_INV_FADE_DISTANCE _SoftParticleFadeParams.y
			#define CAMERA_NEAR_FADE _CameraFadeParams.x
			#define CAMERA_INV_FADE_DISTANCE _CameraFadeParams.y
			#define GEO_NEAR_RESIZE _GeometryParams.x
			#define GEO_INV_RESIZE_DISTANCE _GeometryParams.y
			#define GEO_NEAR_FADE _GeometryParams.z
			#define	GEO_INV_FADE_DISTANCE _GeometryParams.w

			v2f vert(appdata v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.uv = v.uv.xy;
#if defined(_FADING_ON)
				float4 clipPosition = UnityObjectToClipPos(v.vertex);
				o.projectedPosition = ComputeScreenPos(clipPosition);
				COMPUTE_EYEDEPTH(o.projectedPosition.z);
#endif
#if _TWINKLE_ON
				o.color.a *= 1.0 + _TwinkleAmount * cos((_TwinkleSpeed * (_Time[1] + v.uv.z)) * 6.28);
#endif
				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
			}

			half4 frag(v2f i) : SV_Target{
				half4 mask = tex2D(_MainTex, i.uv);
				half4 col = mask.r * i.color + mask.g * _Brightness;
				col.a = i.color.a * mask.a;
#if defined(_FADING_ON)
				// Per-pixel camera fade
				float cameraFade = saturate((i.projectedPosition.z - CAMERA_NEAR_FADE) * CAMERA_INV_FADE_DISTANCE);
				col.a *= cameraFade;

#if defined(SOFTPARTICLES_ON)
				// Depth-based soft particles
				float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projectedPosition)));
				float softFade = saturate(((sceneZ - SOFT_PARTICLE_NEAR_FADE) - i.projectedPosition.z) * SOFT_PARTICLE_INV_FADE_DISTANCE);
				col.a *= softFade;
#endif
#endif

				UNITY_APPLY_FOG_COLOR(i.fogCoord, col, COLOR_CLEAR);

				return col;
			}
			ENDCG
		}
	}

	Fallback "VertexLit"
	CustomEditor "Andtech.StarPack.Editor.ParticleStandardGlowShaderGUI"
}
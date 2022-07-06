using UnityEditor;
using UnityEngine;

namespace Andtech.StarPack.Editor
{

	public class ParticleStandardGlowShaderGUI : BaseShaderGUI
	{

		protected static class Keywords
		{
			public const string _FADING_ON = nameof(_FADING_ON);
			public const string _TWINKLE_ON = nameof(_TWINKLE_ON);
		}

		protected static class Styles
		{
			public static GUIContent requiredVertexStreamsText = EditorGUIUtility.TrTextContent("Required Vertex Streams");
			public static GUIContent streamPositionText = EditorGUIUtility.TrTextContent("Position (POSITION.xyz)");
			public static GUIContent streamColorText = EditorGUIUtility.TrTextContent("Color (COLOR.xyzw)");
			public static GUIContent streamColorInstancedText = EditorGUIUtility.TrTextContent("Color (INSTANCED0.xyzw)");
			public static GUIContent streamUVText = EditorGUIUtility.TrTextContent("UV (TEXCOORD0.xy)");
			public static GUIContent streamRandomX = EditorGUIUtility.TrTextContent("StableRandom.x (TEXCOORD0.z)");
		}

		// Main Options
		private ShaderProperty _MainTex;
		private ShaderProperty _Color;
		private ShaderProperty _SoftParticlesEnabled;
		private ShaderProperty _SoftParticlesNearFadeDistance;
		private ShaderProperty _SoftParticlesFarFadeDistance;
		private ShaderProperty _CameraFadingEnabled;
		private ShaderProperty _CameraNearFadeDistance;
		private ShaderProperty _CameraFarFadeDistance;
		private ShaderProperty _Brightness;
		// Twinkle Options
		private ShaderProperty _TwinkleEnabled;
		private ShaderProperty _TwinkleAmount;
		private ShaderProperty _TwinkleSpeed;
		// Internal
		private ShaderProperty _SoftParticleFadeParams;
		private ShaderProperty _CameraFadeParams;

		protected override void FindProperties()
		{
			// Main Options
			_MainTex = GetProperty(nameof(_MainTex))
				.SetTextContent("Glow Mask", "Colored area (R) white area (G).");
			_Color = GetProperty(nameof(_Color))
				.SetTextContent("Color", "The color of the bullet. This value should typically have a high HDR intensity.");
			_SoftParticlesEnabled = GetProperty(nameof(_SoftParticlesEnabled))
				.SetTextContent("Soft Particles", "Fade out geometry when it gets close to the surface of objects written into the depth buffer.");
			_SoftParticlesNearFadeDistance = GetProperty(nameof(_SoftParticlesNearFadeDistance))
				.SetTextContent("Near Fade", "Soft Particles near fade distance.");
			_SoftParticlesFarFadeDistance = GetProperty(nameof(_SoftParticlesFarFadeDistance))
				.SetTextContent("Far Fade", "Soft Particles far fade distance.");
			_CameraFadingEnabled = GetProperty(nameof(_CameraFadingEnabled))
				.SetTextContent("Camera Fading", "Fade out geometry when it gets close to the camera?");
			_CameraNearFadeDistance = GetProperty(nameof(_CameraNearFadeDistance))
				.SetTextContent("Near Fade", "Camera near fade distance.");
			_CameraFarFadeDistance = GetProperty(nameof(_CameraFarFadeDistance))
				.SetTextContent("Far Fade", "Camera far fade distance.");
			_Brightness = GetProperty(nameof(_Brightness))
				.SetTextContent("Brightness", "Intensity of white areas.");
			// Twinkle Options
			_TwinkleEnabled = GetProperty(nameof(_TwinkleEnabled))
				.SetTextContent("Enable Twinkle", "Enable the twinkle effect.");
			_TwinkleAmount = GetProperty(nameof(_TwinkleAmount))
				.SetTextContent("Twinkle Intensity", "Intensity of the twinkle effect.");
			_TwinkleSpeed = GetProperty(nameof(_TwinkleSpeed))
				.SetTextContent("Twinkle Speed", "Rate of the twinkle effect.");
			// Internal
			_SoftParticleFadeParams = GetProperty(nameof(_SoftParticleFadeParams));
			_CameraFadeParams = GetProperty(nameof(_CameraFadeParams));
		}

		protected override void DrawGUI()
		{
			// Main Options
			GUILayout.Label("Main Options", EditorStyles.boldLabel);
			MainTex();
			Brightness();
			SoftParticles();
			CameraFading();
			EditorGUILayout.Space();

			// Twinkle Options
			GUILayout.Label("Twinkle Options", EditorStyles.boldLabel);
			Twinkle();
			EditorGUILayout.Space();

			// Advanced Options
			GUILayout.Label("Advanced Options", EditorStyles.boldLabel);
			Instancing();
			EditorGUILayout.Space();

			// Particle System
			VertexStreams();

			void MainTex()
			{
				DrawProperty(_MainTex);
			}

			void Brightness()
			{
				DrawProperty(_Brightness);
			}

			void SoftParticles()
			{
				DrawProperty(_SoftParticlesEnabled);
				if (_SoftParticlesEnabled.Toggle)
				{
					DrawProperty(_SoftParticlesNearFadeDistance, DefaultIndentation);
					DrawProperty(_SoftParticlesFarFadeDistance, DefaultIndentation);
				}
			}

			void CameraFading()
			{
				DrawProperty(_CameraFadingEnabled);
				if (_CameraFadingEnabled.Toggle)
				{
					DrawProperty(_CameraNearFadeDistance, DefaultIndentation);
					DrawProperty(_CameraFarFadeDistance, DefaultIndentation);
				}
			}

			void Twinkle()
			{
				DrawProperty(_TwinkleEnabled);
				if (_TwinkleEnabled.Toggle)
				{
					DrawProperty(_TwinkleAmount, DefaultIndentation);
					DrawProperty(_TwinkleSpeed, DefaultIndentation);
				}
			}

			void Instancing()
			{
				MaterialEditor.EnableInstancingField();
			}

			void VertexStreams()
			{
				GUILayout.Label(Styles.requiredVertexStreamsText, EditorStyles.boldLabel);
				bool isTwinkleOn = _TwinkleEnabled.Toggle;

				GUILayout.Label(Styles.streamPositionText, EditorStyles.label);
				GUILayout.Label(Styles.streamColorText, EditorStyles.label);
				GUILayout.Label(Styles.streamUVText, EditorStyles.label);
				if (isTwinkleOn)
				{
					GUILayout.Label(Styles.streamRandomX, EditorStyles.label);
				}
			}
		}

		protected override void MaterialChanged()
		{
			SetMaterialKeywords(Target);
		}

		private void SetMaterialKeywords(Material material)
		{
			FadingOn();
			SoftParticles();
			CameraFading();
			Twinkle();

			void FadingOn()
			{
				if (_SoftParticlesEnabled.Toggle || _CameraFadingEnabled.Toggle)
				{
					material.EnableKeyword(Keywords._FADING_ON);
				}
				else
				{
					material.DisableKeyword(Keywords._FADING_ON);
				}
			}

			void SoftParticles()
			{
				Vector2 softParticleFadeParams;
				if (_SoftParticlesEnabled.Toggle)
				{
					softParticleFadeParams = GetFadeParams(_SoftParticlesNearFadeDistance.Float, _SoftParticlesFarFadeDistance.Float);
				}
				else
				{
					softParticleFadeParams = DefaultFadeParams;
				}

				_SoftParticleFadeParams.Vector = softParticleFadeParams;
			}

			void CameraFading()
			{
				Vector2 cameraFadeParams;
				if (_CameraFadingEnabled.Toggle)
				{
					cameraFadeParams = GetFadeParams(_CameraNearFadeDistance.Float, _CameraFarFadeDistance.Float);
				}
				else
				{
					cameraFadeParams = DefaultFadeParams;
				}

				_CameraFadeParams.Vector = cameraFadeParams;
			}

			void Twinkle()
			{
				if (_TwinkleEnabled.Toggle)
				{
					material.EnableKeyword(Keywords._TWINKLE_ON);
				}
				else
				{
					material.DisableKeyword(Keywords._TWINKLE_ON);
				}
			}
		}
	}
}

using UnityEditor;
using UnityEngine;

namespace Andtech.StarPack.Editor
{

	public abstract class BaseShaderGUI : ShaderGUI
	{

		/// <summary>
		/// Wrapper for working with properties within the shader.
		/// </summary>
		protected class ShaderProperty
		{
			public readonly string Name;
			public readonly MaterialProperty MaterialProperty;
			public string Keyword { get; set; }
			public string DisplayName { get; set; }
			public string Tooltip { get; set; }
			public float Float
			{
				get => MaterialProperty.floatValue;
				set => MaterialProperty.floatValue = value;
			}
			public bool Toggle
			{
				get => MaterialProperty.floatValue > 0.0F;
				set => MaterialProperty.floatValue = value ? 1.0F : 0.0F;
			}
			public Vector4 Vector
			{
				get => MaterialProperty.vectorValue;
				set => MaterialProperty.vectorValue = value;
			}
			public GUIContent Content => EditorGUIUtility.TrTextContent(DisplayName, Tooltip);

			public ShaderProperty(string name, MaterialProperty materialProperty)
			{
				Name = name;
				MaterialProperty = materialProperty;
				Keyword = name;
				DisplayName = name;
				Tooltip = string.Empty;
			}

			public ShaderProperty SetTextContent(string displayName, string tooltip)
			{
				DisplayName = displayName;
				Tooltip = tooltip;
				return this;
			}

			public ShaderProperty SetKeyword(string value)
			{
				Keyword = value;
				return this;
			}
		}

		protected MaterialEditor MaterialEditor { get; set; }
		protected Material Target { get; set; }
		protected MaterialProperty[] MaterialProperties { get; set; }
		protected bool FirstTimeApply { get; set; } = true;

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			MaterialEditor = materialEditor;
			Target = materialEditor.target as Material;
			MaterialProperties = properties;

			FindProperties();

			if (FirstTimeApply)
			{
				MaterialChanged();
				FirstTimeApply = false;
			}

			ShaderPropertiesGUI();
		}

		protected abstract void FindProperties();

		protected abstract void DrawGUI();

		protected abstract void MaterialChanged();

		protected virtual void ShaderPropertiesGUI()
		{
			EditorGUIUtility.labelWidth = 0.0F;

			EditorGUI.BeginChangeCheck();
			DrawGUI();
			if (EditorGUI.EndChangeCheck())
			{
				MaterialChanged();
			}
		}

		protected ShaderProperty GetProperty(string propertyName)
		{
			var materialProperty = FindProperty(propertyName, MaterialProperties);
			return new ShaderProperty(propertyName, materialProperty);
		}

		protected void DrawProperty(ShaderProperty property, int indentation = 0)
		{
			MaterialEditor.ShaderProperty(property.MaterialProperty, property.Content, indentation);
		}

		protected static readonly int DefaultIndentation = 2;
		protected static readonly Vector2 DefaultFadeParams = new Vector2(0.0F, Mathf.Infinity);
		protected static Vector2 GetFadeParams(float near, float far) => new Vector4(near, 1.0F / (far - near));
	}
}

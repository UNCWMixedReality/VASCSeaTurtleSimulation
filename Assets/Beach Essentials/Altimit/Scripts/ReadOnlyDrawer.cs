using System;
using System.CodeDom;
using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute
{

}

#if !UNITY_EDITOR
  public sealed class CustomPropertyDrawer : Attribute
  {
    internal System.Type m_Type;
    internal bool m_UseForChildren;

    /// <summary>
    ///   <para>Tells a PropertyDrawer or DecoratorDrawer class which run-time class or attribute it's a drawer for.</para>
    /// </summary>
    /// <param name="type">If the drawer is for a custom Serializable class, the type should be that class. If the drawer is for script variables with a specific PropertyAttribute, the type should be that attribute.</param>
    /// <param name="useForChildren">If true, the drawer will be used for any children of the specified class unless they define their own drawer.</param>
    public CustomPropertyDrawer(System.Type type)
    {
      this.m_Type = type;
    }

    /// <summary>
    ///   <para>Tells a PropertyDrawer or DecoratorDrawer class which run-time class or attribute it's a drawer for.</para>
    /// </summary>
    /// <param name="type">If the drawer is for a custom Serializable class, the type should be that class. If the drawer is for script variables with a specific PropertyAttribute, the type should be that attribute.</param>
    /// <param name="useForChildren">If true, the drawer will be used for any children of the specified class unless they define their own drawer.</param>
    public CustomPropertyDrawer(System.Type type, bool useForChildren)
    {
      this.m_Type = type;
      this.m_UseForChildren = useForChildren;
    }
 }
#endif

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer
#if UNITY_EDITOR
: PropertyDrawer
#endif
{
#if UNITY_EDITOR
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
#endif
}
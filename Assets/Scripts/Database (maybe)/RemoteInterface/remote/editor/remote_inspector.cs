/*

    Daniel Vaughn
    UNCW VR_Research_Team

    remote_inspector.cs

*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEditor;

[Serializable, CustomEditor(typeof(remote))]
public class remote_inspector : Editor
{

    public override void OnInspectorGUI()
    {

        remote obj = ((remote)target);

        if (GUILayout.Button("Get Components"))
        {
            obj.getAttributes();
        }

        GUILayout.Label("Remote Settings");

        foreach (RemoteComponents rc in obj.settings.components)
        {

            EditorGUI.indentLevel = 0;
            rc.allowModification = EditorGUILayout.ToggleLeft("   " + rc.ToString(), rc.allowModification);

            if (rc.allowModification)
            {
                foreach (RemoteConfigurable rconfig in rc.settings)
                {
                    EditorGUI.indentLevel = 2;
                    rconfig.allowModification = EditorGUILayout.ToggleLeft(rconfig.ToString(), rconfig.allowModification);
                }
            }

        }

    }

}

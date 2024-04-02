using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateObjects))]
public class ObjectCreator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

#region Deprecated
        // Deprecated
        if(GUILayout.Button("Generate objects"))
        {
            var objCreator = (CreateObjects)target;
            objCreator.GenerateObjects();
        }
#endregion

        if(GUILayout.Button("Generate objects on ground"))
        {
            var objCreator = (CreateObjects)target;
            objCreator.GenerateObjectsOnGrass();
        }

    }
}

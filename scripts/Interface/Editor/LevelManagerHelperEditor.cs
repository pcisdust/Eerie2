using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Assets.IntenseTPS.Scripts.Level;
using Assets.IntenseTPS.Scripts.AI;

[CustomEditor(typeof(LevelManagerHelper))]
public class LevelManagerHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var _ob = target as LevelManagerHelper;
        var _levelLogic = _ob.GetComponent<LevelLogic>();
        if (GUILayout.Button("Auto get all variable", EditorStyles.miniButton))
        {
            if (_levelLogic)
            {
                _levelLogic.AutoGet();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}

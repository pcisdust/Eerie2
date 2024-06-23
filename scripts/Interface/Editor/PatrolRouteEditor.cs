using UnityEditor;
using UnityEngine;
using Assets.IntenseTPS.Scripts.AI;

[CustomEditor(typeof(PatrolRoute))]
public class PatrolRouteEditor : Editor
{
    private SerializedProperty patrolPositions;
    private SerializedProperty patrolLoop;
    private SerializedProperty fast;
    private SerializedProperty randomRadius;
    private SerializedProperty follow;
    private PatrolRoute _pr;

    private void OnEnable()
    {
        patrolPositions = serializedObject.FindProperty("patrolPositions");
        patrolLoop = serializedObject.FindProperty("patrolLoop");
        fast = serializedObject.FindProperty("fast");
        randomRadius = serializedObject.FindProperty("randomRadius");
        follow = serializedObject.FindProperty("follow");
        _pr = target as PatrolRoute;
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < patrolPositions.arraySize; i++)
        {
            //point
            patrolPositions.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(patrolPositions.GetArrayElementAtIndex(i).vector3Value, Quaternion.identity);
            Handles.Label(patrolPositions.GetArrayElementAtIndex(i).vector3Value, new GUIContent(i.ToString()));
            Handles.DrawWireArc(patrolPositions.GetArrayElementAtIndex(i).vector3Value, Vector3.up, Vector3.right, 360, randomRadius.floatValue);
            //line
            if (i < patrolPositions.arraySize - 1|| patrolLoop.boolValue)
            {
                var pos = patrolPositions.GetArrayElementAtIndex(i).vector3Value;
                var pos2 = (i < patrolPositions.arraySize - 1 ? patrolPositions.GetArrayElementAtIndex(i + 1) : patrolPositions.GetArrayElementAtIndex(0)).vector3Value;
                Handles.DrawDottedLine(pos, pos2, 1f);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        fast.boolValue = EditorGUILayout.Toggle("Fast", fast.boolValue);
        patrolLoop.boolValue = EditorGUILayout.Toggle("Patrol Loop", patrolLoop.boolValue);
        randomRadius.floatValue = EditorGUILayout.FloatField("Random Radius", randomRadius.floatValue);
        follow.objectReferenceValue = EditorGUILayout.ObjectField("Follow object",null, typeof(Transform),true);
        GUILayout.Space(5);
        if (GUILayout.Button("Add Patrol Point", EditorStyles.miniButton))
        {
            patrolPositions.InsertArrayElementAtIndex(patrolPositions.arraySize);
            patrolPositions.GetArrayElementAtIndex(patrolPositions.arraySize - 1).vector3Value =
                (patrolPositions.arraySize == 1 ? _pr.transform.position+Vector3.up : patrolPositions.GetArrayElementAtIndex(patrolPositions.arraySize - 1).vector3Value + Vector3.right);
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Delet Patrol Point", EditorStyles.miniButton))
        {
            if(patrolPositions.arraySize!=0)
            patrolPositions.DeleteArrayElementAtIndex(patrolPositions.arraySize-1);
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Drop Points Down", EditorStyles.miniButton))
        {
            for (int i = 0; i < patrolPositions.arraySize; i++)
            {
                var pos = patrolPositions.GetArrayElementAtIndex(i).vector3Value;
                RaycastHit hit;
                if (Physics.Raycast(pos, Vector3.down, out hit))
                    patrolPositions.GetArrayElementAtIndex(i).vector3Value = hit.point + Vector3.up * 0.1f;
            }
        }
        GUILayout.Space(5);

        serializedObject.ApplyModifiedProperties();
    }
}
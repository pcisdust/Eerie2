using UnityEditor;
using UnityEngine;
using Assets.IntenseTPS.Scripts.Common;

namespace Assets.IntenseTPS.Scripts.Level
{
    [CustomEditor(typeof(ItemBuilder))]
    public class ItemBuilderEditor : Editor
    {
        private SerializedProperty itemName;
        private SerializedProperty addData;
        private SerializedProperty count;
        private SerializedProperty maxcount;
        private SerializedProperty positions;
        private SerializedProperty directlyIntoLocalPlayerInventory;
        private SerializedProperty startMove;
        private SerializedProperty offset;
        private SerializedProperty height;
        private ItemBuilder _builder;
        void OnEnable()
        {
            itemName = serializedObject.FindProperty("itemName");
            addData = serializedObject.FindProperty("addData");
            count = serializedObject.FindProperty("count");
            maxcount = serializedObject.FindProperty("maxCount");
            positions= serializedObject.FindProperty("points");
            startMove = serializedObject.FindProperty("startMove");
            offset = serializedObject.FindProperty("offset");
            height = serializedObject.FindProperty("height");
            directlyIntoLocalPlayerInventory = serializedObject.FindProperty("directlyIntoLocalPlayerInventory");
            _builder = target as ItemBuilder;
        }
        private void OnSceneGUI()
        {
            for (int i = 0; i < positions.arraySize; i++)
            {
                positions.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(positions.GetArrayElementAtIndex(i).vector3Value, Quaternion.identity);
                Handles.Label(positions.GetArrayElementAtIndex(i).vector3Value, new GUIContent(itemName.stringValue));
            }
            serializedObject.ApplyModifiedProperties();
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            directlyIntoLocalPlayerInventory.boolValue = EditorGUILayout.Toggle("Directly into local player inventory", directlyIntoLocalPlayerInventory.boolValue);
            itemName.stringValue = EditorGUILayout.TextField("Item name:", itemName.stringValue);
            EditorGUILayout.Space();
            count.intValue = EditorGUILayout.IntField("Count", count.intValue);
            maxcount.boolValue= EditorGUILayout.Toggle("Max Count", maxcount.boolValue);
            EditorGUILayout.LabelField("item code:");
            GUIStyle style = new GUIStyle(EditorStyles.textArea)
            {
                wordWrap = true
            };
            addData.stringValue = EditorGUILayout.TextArea(addData.stringValue, style ,GUILayout.Height(300));
            GUILayout.Space(25);
            if (GUILayout.Button("Add character def data", EditorStyles.miniButton))
            {
                addData.stringValue = "aiLeader/health/willPower/vehicleIn/seatIndex/overrideInv/faction/maxHealth/needSave";
            }
            if (GUILayout.Button("Add autogun def data", EditorStyles.miniButton))
            {
                addData.stringValue = "aileader/heal/factionCode/maxHeal";
            }
            if (GUILayout.Button("Add player def data", EditorStyles.miniButton))
            {
                addData.stringValue =LevelManager.defPlayerData;
            }
            if (GUILayout.Button("Add vehicle def data", EditorStyles.miniButton))
            {
                addData.stringValue = "aiLeader/vehicleCode/health/faction/ammo0/ammo1/ammo2";
            }
            GUILayout.Space(5);
            EditorGUILayout.LabelField("faction code: 0-Friendly 1-Hostile 2-ThirdParty");
            EditorGUILayout.LabelField("generate point number: "+ positions.arraySize.ToString());
            if (GUILayout.Button("Add Generate Point", EditorStyles.miniButton))
            {
                positions.InsertArrayElementAtIndex(positions.arraySize);
                positions.GetArrayElementAtIndex(positions.arraySize - 1).vector3Value =
                    (positions.arraySize == 1 ? _builder.transform.position + Vector3.right : positions.GetArrayElementAtIndex(positions.arraySize - 1).vector3Value + Vector3.right);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Delet Generate Point", EditorStyles.miniButton))
            {
                if (positions.arraySize != 0)
                    positions.DeleteArrayElementAtIndex(positions.arraySize - 1);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Drop Points Down", EditorStyles.miniButton))
            {
                RaycastHit hit0;
                if (Physics.Raycast(_builder.transform.position, Vector3.down, out hit0))
                    _builder.transform.position = hit0.point + Vector3.up * 0.1f;
                for (int i = 0; i < positions.arraySize; i++)
                {
                    var pos = positions.GetArrayElementAtIndex(i).vector3Value;
                    RaycastHit hit;
                    if (Physics.Raycast(pos, Vector3.down, out hit))
                        positions.GetArrayElementAtIndex(i).vector3Value = hit.point + Vector3.up * 0.1f;
                }
            }
            if (GUILayout.Button("Drop Points Down Above", EditorStyles.miniButton))
            {
                RaycastHit hit0;
                if (Physics.Raycast(_builder.transform.position, Vector3.down, out hit0))
                    _builder.transform.position = hit0.point + Vector3.up * 0.35f;
                for (int i = 0; i < positions.arraySize; i++)
                {
                    var pos = positions.GetArrayElementAtIndex(i).vector3Value;
                    RaycastHit hit;
                    if (Physics.Raycast(pos, Vector3.down, out hit))
                        positions.GetArrayElementAtIndex(i).vector3Value = hit.point + Vector3.up * 0.35f;
                }
            }
            EditorGUILayout.Space();
            startMove.boolValue = EditorGUILayout.Toggle("Ues start move", startMove.boolValue);
            offset.floatValue = EditorGUILayout.FloatField("move offset", offset.floatValue);
            height.floatValue = EditorGUILayout.FloatField("move height", height.floatValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
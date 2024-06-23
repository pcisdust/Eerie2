using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    //ItemBuilderEditor
    public class ItemBuilder : MonoBehaviour
    {
        public bool muitPlayerSpawnPoint = false;
        public bool directlyIntoLocalPlayerInventory = false;
        public string itemName;
        public string addData;
        public int count=1;
        public bool startMove = false;
        public float offset = 2f;
        public float height = 3f;
        public bool maxCount = false;
        public Vector3[] points=new Vector3[0];
    }
}

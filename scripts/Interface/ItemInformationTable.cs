using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    public enum UniversalItemType { Weapon, Accessory, Tool, Ammo, Magazine, Vest, Armor, Clothing, Helmet, Character, Vehicle, LevelLogic, Other }
    public class ItemInformationTable : MonoBehaviour
    {
        public string itemName;
        public string itemTag;
        public UniversalItemType itemType;
        public float weight = 0f;
        public int overlap = 1;
        public int variant =1;
        public bool iconBaseOnFpsObject = true;
        public bool canPickUp = true;
    }
}
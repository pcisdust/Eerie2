using System.Collections.Generic;
using UnityEngine;
using Assets.IntenseTPS.Scripts.AI;

namespace Assets.IntenseTPS.Scripts.Common
{
    public class CharacterProperty : MonoBehaviour
    {
        [Range(0, 2)]
        public int mapIconType = 0;
        public string showName = "Inventory";
        public string inventoryFileName = "null";
        public Avatar avatar;
        [Range(0.85f, 1.15f)]
        public float bodySize = 1f;
        [Range(0f,1f)]
        public float upBodySlim = 0f;
        public string[] baseParts = new string[] { "null", "null", "null" };
        public string[] characterPartsTags = new string[] { "hat", "face_decoration", "earPhone", "vest", "female_top", "watch", "glove", "female_bottom", "legwear", "female_shoes" };
        public Faction faction;
        public List<AiVoice> aiVoiceSets;
    }
}
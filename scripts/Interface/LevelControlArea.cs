using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelControlArea : MonoBehaviour
    {
        public float areaRadius = 5f;
        public List<string> conditions;
        public List<int> triggerPlayers;
        public List<int> suppressionPlayers;
        public string enterAction;
        [Space]
        public bool needHold=false;
        public string holdVariable;
        public int variableValueBiggerThan=0;
        public string exitAction;

        [HideInInspector]
        public bool holding=false;
        public bool active = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.AI
{
    public enum AiVoiceType
    { SpotEnemy, Reload, SomeOneDown, Down, Idle, Follow, Stop, Yes, No, PointA, PointB, PointC }
    [CreateAssetMenu(fileName = "AiVoice", menuName = "EERIE2/AiVoice", order = 3)]
    public class AiVoice : ScriptableObject
    {
        public AiVoiceType voiceType;
        public List<AudioClip> allVoices;
        public List<string> allLines;
        public List<int> allFace;
    }
}
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Eerie2
{
    public class DialogueSequence : MonoBehaviour
    {
        public List<DialogueLine> dialogueLines;
    }

    [Serializable]
    public class DialogueLine
    {
        public string text;
        public string talkName;
        public int face = 0;
        public AudioClip audioClip;
    }
}
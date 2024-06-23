using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Level
{
    public enum LevelSoundType { SoundEffect, Environment, Bgm, Voice, Ui }
    public class LevelSound : MonoBehaviour
    {
        public LevelSoundType soundType;
        void Start()
        {
            LevelManager.LevelSound(transform, soundType);
        }
    }
}
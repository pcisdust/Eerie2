using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Level
{
    public class PictureChanger : MonoBehaviour
    {
        public Renderer mainRenderer;
        public Texture2D picture;
        public int mainIndex = 0;
        void Start()
        {
            if (mainRenderer&&picture)
            {
                MaterialPropertyBlock prop = new();
                prop.SetTexture("_BaseMap", picture);
                mainRenderer.SetPropertyBlock(prop, mainIndex);
            }

        }
    }
}
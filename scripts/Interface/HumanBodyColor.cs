using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Common
{
    public class HumanBodyColor : MonoBehaviour
    {
        public Color skinColor = Color.gray;
        public Color hairColor = Color.gray;
        public List<Renderer> hairRenderers;
        public Texture customFace;
        public Texture customEye;
        public string mainColorName = "_MainColor";
        [Range(0,100)]
        public float additionalVariable = 0f;
        private MaterialPropertyBlock hairProp;
        public void StartUp()
        {
            hairProp = new MaterialPropertyBlock();
            hairProp.SetColor(mainColorName, hairColor);
            if (hairRenderers.Count != 0)
            {
                foreach (var item in hairRenderers)
                {
                    item.SetPropertyBlock(hairProp);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.IntenseTPS.Scripts.Level;
namespace Assets.IntenseTPS.Scripts.Common
{
    public class HumanBodyColor : MonoBehaviour
    {
        [Range(0,5)]
        public int darkSkin = 0;
        public Color hairColor = Color.gray;
        public List<Renderer> hairRenderers;
        [Space]
        public bool needCelShading = false;
        public List<Texture2D> hairTextures;
        public List<Texture2D> hairNormals;
        [Space]
        public Texture2D customEye;
        [Range(0,100)]
        public float additionalVariable = 0f;

        const string mainColorName = "_MainColor";
        const string celShadingTexName = "_MainTex";
        const string celShadingNormalName = "_NormalMap";
        private bool init = false;
        public void StartUp()
        {
            if (init) return;
            init = true;
            if (hairRenderers.Count != 0)
            {
                for (int i=0; i < hairRenderers.Count;i++)
                {
                    var prop = new MaterialPropertyBlock();
                    prop.SetColor(mainColorName, hairColor);
                    if (needCelShading)
                    {
                        hairRenderers[i].material = LevelManager.GetCelShadingMat(3);
                        prop.SetTexture(celShadingTexName, hairTextures[i]);
                        if(hairNormals[i])
                            prop.SetTexture(celShadingNormalName, hairNormals[i]);
                    }
                    hairRenderers[i].SetPropertyBlock(prop);
                }
            }
        }
    }
}
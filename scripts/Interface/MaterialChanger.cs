using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    public class MaterialChanger : MonoBehaviour
    {
        public Renderer curRenderer;
        public Material[] materials0;
        public Material[] materials1;
        public Material[] materials2;
        public Material[] materials3;
        public void ChangeMaterial(int _type)
        {
            if (!curRenderer) curRenderer = GetComponent<Renderer>();
            Material[] newMaterials = new Material[curRenderer.materials.Length];
            for (int i = 0; i < curRenderer.materials.Length; i++)
            {
                if (i == 0) { newMaterials[i] = materials0[_type]; }
                else if (i == 1) { newMaterials[i] = materials1[_type]; }
                else if (i == 2) { newMaterials[i] = materials2[_type]; }
                else if (i == 3) { newMaterials[i] = materials3[_type]; }
                else break;
            }
            curRenderer.sharedMaterials = newMaterials;
        }
    }
}
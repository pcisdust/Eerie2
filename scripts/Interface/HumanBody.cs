using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    public class HumanBody : MonoBehaviour
    {
        [Serializable]
        public class StockingParameter
        {
            public string parameterName;
            public float denier=1;
            public float rimPower=4;
            public Texture2D mask;
        }
        public bool customFace = false;
        public SkinnedMeshRenderer meshRenderer;
        public HumanBodyProperty humanBodyProperty;
        public string mainTexName = "_MainTex";
        public string mainNormalName = "_Normal";
        public string mainColorName = "_MainColor";
        public Texture2D normal;
        public Texture2D alterNormal;
        private MaterialPropertyBlock prop;
        private bool _inited = false;
        public void StartUp(HumanBodyColor humanBodyColor) 
        {
            Init();
            if (humanBodyColor.customFace&& humanBodyProperty.faceIndex>= 0&& customFace)
            {
                MaterialPropertyBlock prop = new();
                prop.SetTexture(mainTexName, humanBodyColor.customFace);
                meshRenderer.SetPropertyBlock(prop, humanBodyProperty.faceIndex);
            }
            if (humanBodyColor.customEye&&humanBodyProperty.eyeBallIndex>=0) 
            {
                MaterialPropertyBlock prop = new();
                prop.SetTexture(mainTexName, humanBodyColor.customEye);
                meshRenderer.SetPropertyBlock(prop, humanBodyProperty.eyeBallIndex);
            }
            if (humanBodyProperty.eyebrowIndex >= 0)
            {
                MaterialPropertyBlock prop = new();
                prop.SetColor(mainColorName, humanBodyColor.hairColor*(new Color(0.6f,0.6f,0.6f,1f)));
                meshRenderer.SetPropertyBlock(prop, humanBodyProperty.eyebrowIndex);
            }
            if (humanBodyProperty.additionalVariable >= 0) 
            {
                meshRenderer.SetBlendShapeWeight(humanBodyProperty.additionalVariable, humanBodyColor.additionalVariable);
            }
        }
        public void ApplyMaterialVarieta(Vector4 _upperVector4, Vector4 _lowerVector4)
        {
            Init();
            prop.SetVector("_MaskUpperValue", _upperVector4);
            prop.SetVector("_MaskLegValue", _lowerVector4);
            meshRenderer.SetPropertyBlock(prop,humanBodyProperty.bodyIndex);
        }
        public void ChangeMaterial(string _name,Color _color,float _skinColor)
        {
            Init();
            bool get = false;
            foreach(var item in humanBodyProperty.stockingParameters) 
            {
                if (item.parameterName == _name) 
                {
                    prop.SetTexture("_DensityTexture", item.mask);
                    prop.SetColor("_StockingColorTint", _color);
                    prop.SetFloat("_Denier",item.denier);
                    prop.SetFloat("_RimPower", item.rimPower);
                    prop.SetColor("_StockingColorTint", _color);
                    prop.SetFloat("_SkinColorTint", _skinColor);
                    get = true;
                    break;
                }
            }
            if (!get)
            {
                if (normal)
                    prop.SetTexture(mainNormalName, normal);

                prop.SetTexture("_DensityTexture", humanBodyProperty.noStockingTexture);
            }
            else
            {
                if (alterNormal)
                    prop.SetTexture(mainNormalName, alterNormal);
            }
            meshRenderer.SetPropertyBlock(prop, humanBodyProperty.bodyIndex);
        }
        private void Init()
        {
            if (!_inited)
            {
                if (!meshRenderer) meshRenderer = GetComponent<SkinnedMeshRenderer>();
                _inited = true;
                prop = new MaterialPropertyBlock();
            }
        }
    }
}
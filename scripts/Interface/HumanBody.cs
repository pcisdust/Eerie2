using System;
using Assets.IntenseTPS.Scripts.Level;
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

        public int additionalVariableIndex = -1;
        [Header("Cel render")]
        public bool needCelShading = false;
        [Space]
        [Header("face")]
        public int faceIndex = -1;
        public Texture2D faceTexture;
        public Texture2D faceGloss;
        [Space]
        public int eyeBallIndex = -1;
        public int eyebrowIndex = -1;
        [Space]
        public SkinnedMeshRenderer meshRenderer;
        [Header("advanced body render")]
        public bool advanced = false;
        public int bodyIndex = 0;
        public HumanBodyProperty humanBodyProperty;
        public const string _name = "2";

        private MaterialPropertyBlock prop;
        private bool _inited = false;
        const string advancedNormalName = "_Normal";
        const string celShadingTexName = "_MainTex";
        const string celShadingColorName = "_MainColor";
        const string celShadingGlossName = "_MaskGloss";

        public void StartUp(HumanBodyColor humanBodyColor) 
        {
            Init();
            Material[] materials = meshRenderer.sharedMaterials;
            if (faceIndex >= 0)
            {
                if (needCelShading)
                {
                    materials[faceIndex] = LevelManager.GetCelShadingMat(0);
                    MaterialPropertyBlock prop = new();
                    prop.SetTexture(celShadingTexName, faceTexture);
                    prop.SetTexture(celShadingGlossName, faceGloss);
                    meshRenderer.SetPropertyBlock(prop, faceIndex);
                }
            }
            if (eyeBallIndex >= 0)
            {
                if (needCelShading)
                    materials[eyeBallIndex] = LevelManager.GetCelShadingMat(1);
                if (humanBodyColor)
                {
                    MaterialPropertyBlock prop = new();
                    prop.SetTexture(celShadingTexName, humanBodyColor.customEye);
                    meshRenderer.SetPropertyBlock(prop, eyeBallIndex);
                }
            }
            if (eyebrowIndex >= 0)
            {
                if (needCelShading)
                    materials[eyebrowIndex] = LevelManager.GetCelShadingMat(2);
                if (humanBodyColor)
                {
                    MaterialPropertyBlock prop = new();
                    prop.SetColor(celShadingColorName, humanBodyColor.hairColor * (new Color(0.6f, 0.6f, 0.6f, 1f)));
                    meshRenderer.SetPropertyBlock(prop, eyebrowIndex);
                }
            }
            if (additionalVariableIndex >= 0 && humanBodyColor)
            {
                meshRenderer.SetBlendShapeWeight(additionalVariableIndex, humanBodyColor.additionalVariable);
            }
            meshRenderer.sharedMaterials = materials;
        }
        public void ApplyMaterialVarieta(Vector4 _upperVector4, Vector4 _lowerVector4)
        {
            if (!advanced) return;
            Init();
            prop.SetVector("_MaskUpperValue", _upperVector4);
            prop.SetVector("_MaskLegValue", _lowerVector4);
            meshRenderer.SetPropertyBlock(prop,bodyIndex);
        }
        public void ChangeMaterial(string _name,Color _color,float _skinColor)
        {
            if (!advanced||!humanBodyProperty) return;
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
                if (humanBodyProperty.normal)
                    prop.SetTexture(advancedNormalName, humanBodyProperty.normal);

                prop.SetTexture("_DensityTexture", humanBodyProperty.noStockingTexture);
            }
            else
            {
                if (humanBodyProperty.alterNormal)
                    prop.SetTexture(advancedNormalName, humanBodyProperty.alterNormal);
            }
            meshRenderer.SetPropertyBlock(prop,bodyIndex);
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
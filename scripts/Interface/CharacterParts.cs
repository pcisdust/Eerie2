using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    public class CharacterParts : MonoBehaviour
    {
        public GameObject oldBones;
        public Animator face;
        public List<SkinnedMeshRenderer> slimMeshRenderers;
        public List<SkinnedMeshRenderer> skinnedMeshRenderers;
        public List<HumanBody> humanBodys;
        public Renderer simplifiedModel;
        public string leftArmObj;
        public string rightArmObj;
        public Vector4 upperBodyMask;
        public Vector4 lowerBodyMask;
        public bool keepHeadSize = false;
        public bool changeMaterial = false;
        public string materialType = "";
        public Color color = Color.white;
        public float skinColor=1f;
        public void SetUp(SkinnedMeshRenderer _base,Transform _lightProbe=null)
        {
            if (skinnedMeshRenderers.Count > 0)
            {
                foreach (var item in skinnedMeshRenderers)
                {
                    if (_lightProbe) 
                    {
                        item.probeAnchor = _lightProbe;
                    }
                    item.rootBone = _base.rootBone;
                    item.bones = _base.bones;
                }
            }
            if (oldBones)
                Destroy(oldBones);
        }
        public void SetUpBody(HumanBodyColor humanBodyColor) 
        {
            if (humanBodyColor)
            {
                humanBodyColor.StartUp();
                if (simplifiedModel)
                {
                    MaterialPropertyBlock prop = new();
                    prop.SetColor("_SkinColor", humanBodyColor.skinColor);
                    prop.SetColor("_HairColor", humanBodyColor.hairColor);
                    simplifiedModel.SetPropertyBlock(prop);
                }
            }
            if (humanBodys.Count != 0)
            {
                foreach (var item in humanBodys)
                {
                    item.StartUp(humanBodyColor);
                }
            }
        }
        public void ChangeSlim(float _slim) 
        {
            if (slimMeshRenderers.Count > 0)
            {
                foreach (var item in slimMeshRenderers)
                {
                    item.SetBlendShapeWeight(0, _slim*100);
                }
            }
        }
        public void CanView(bool _can) 
        {
            if (skinnedMeshRenderers.Count == 0)
                gameObject.SetActive(_can);
            else
            {
                foreach (var item in skinnedMeshRenderers)
                {
                    if (_can)
                        item.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    else { item.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly; }
                }
            }
        }
        public void ApplyMaterialVarieta(Vector4 _upperVector4, Vector4 _lowerVector4)
        {
            if (humanBodys.Count == 0) return;
            foreach (var item in humanBodys) 
            { item.ApplyMaterialVarieta(_upperVector4, _lowerVector4); }
        }
        public void ChangeMaterial(string _name,Color _color,float _skinColor)
        {
            if (humanBodys.Count == 0) return;
            foreach (var item in humanBodys)
            { item.ChangeMaterial(_name,_color,_skinColor); }
        }

    }
}

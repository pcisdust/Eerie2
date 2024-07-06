using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    [CreateAssetMenu(fileName = "HumanBodyProperty", menuName = "EERIE2/HumanBodyProperty", order = 7)]
    public class HumanBodyProperty : ScriptableObject
    {
        public Texture2D normal;
        public Texture2D alterNormal;
        public Texture2D noStockingTexture;
        public List<HumanBody.StockingParameter> stockingParameters;
    }
}
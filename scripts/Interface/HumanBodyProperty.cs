using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Common
{
    [CreateAssetMenu(fileName = "HumanBodyProperty", menuName = "EERIE2/HumanBodyProperty", order = 7)]
    public class HumanBodyProperty : ScriptableObject
    {
        public int bodyIndex = 0;
        public int faceIndex = -1;
        public int eyeBallIndex = -1;
        public int eyebrowIndex = -1;
        public int additionalVariable = -1;
        public Texture2D noStockingTexture;
        public List<HumanBody.StockingParameter> stockingParameters;
    }
}
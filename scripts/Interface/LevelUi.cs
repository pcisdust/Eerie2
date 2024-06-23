using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelUi : MonoBehaviour
    {
        public List<string> showUpConditions;
        [Space]
        public bool onlySwitchColor = false;
        public Image switchColorImage;
        public Color activeColor = Color.white;
        public Color inactiveColor = Color.gray;
        [Space]
        public bool indicator = false;
        public string indicatorVariable = "";
        public Text indicatorText;
        public Slider indicatorSlider;
        public bool timeIndicator = false;

        public void IndicatorSet(int va) 
        {
            if (!indicator) return;
            if (timeIndicator) 
            {
                if (va <= 0)
                {
                   UiShow(false); return;
                }
            }
            if (indicatorSlider)
            {
                if (timeIndicator)
                {
                    indicatorSlider.value = indicatorSlider.maxValue-va;
                }
                else indicatorSlider.value = va;
            }
            if (indicatorText) 
            {
                if (timeIndicator)
                {
                    TimeSpan time = TimeSpan.FromSeconds(va);
                    indicatorText.text = time.ToString("mm\\:ss");
                }
                else indicatorText.text = va.ToString();
            }
        }

        public void UiShow(bool _show) 
        {
            if (_show)
            {
                gameObject.SetActive(true);
                if (onlySwitchColor)
                {
                    switchColorImage.color = activeColor;
                }
            }
            else
            {
                if (!onlySwitchColor) { gameObject.SetActive(false); }
                else { switchColorImage.color = inactiveColor; }
            }
        }
    }
}
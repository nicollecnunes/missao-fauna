using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificuldadeManager : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 3;
        slider.wholeNumbers = true; 

        slider.value = 0;
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    public void OnSliderValueChanged()
    {
        PlayerPrefs.SetInt("NivelDeDificuldade", (int)slider.value);
    }
}

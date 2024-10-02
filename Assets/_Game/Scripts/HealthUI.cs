using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider3D;
    public Slider healthSlider2D;

    public void Start3DSlider(float maxValue)
    {
        healthSlider3D.maxValue = maxValue;
        healthSlider3D.value = maxValue;
    }
    public void Update3DSlider(float value)
    {
        healthSlider3D.value = value;
        //2D = PLAYER ONLY
    }
    public void Update2DSlider(float maxValue, float value)
    {
        if (gameObject.CompareTag("Player"))
        {
            healthSlider2D.maxValue = maxValue;
            healthSlider2D.value = value;
        }

    }
}
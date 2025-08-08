using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        //Sets the value of maxValue on slider equal to health.
        slider.maxValue = health;

        //Sets the value of value on slider equal to health.
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        //Sets the value of value on slider equal to health.
        slider.value = health;
    }
}

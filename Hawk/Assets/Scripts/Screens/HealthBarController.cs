using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Gradient gradient;

    public void  FillImage(float value)
    {
        healthBarImage.fillAmount = value;
        healthBarImage.color = gradient.Evaluate(value);
    }
}

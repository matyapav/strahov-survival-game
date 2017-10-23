using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    [System.Serializable]
    public class ValueEvent : UnityEvent { }

    [Tooltip("Bar name text component")]
    public Text barNameText;
    [Tooltip("Bar rendering image")]
    public Image barValueImage;

    [Tooltip("Called after rendering new value into bar")]
    public ValueEvent onValueChanged;
    [Tooltip("Called after value reaches its minimum")]
    public ValueEvent onMinimumReached;

    public delegate void OnMinimumReached();

    public string barName;
    [Tooltip("Value at the beginning, must be between max value and min value")]
    public float startValue;
    [Tooltip("Check this if you want ot start at maximum value.")]
    public bool startAtMaximum;
    public float maxValue;
    public float minValue;

    private float currentValue;

    private void Start()
    {
        if (startAtMaximum)
        {
            startValue = maxValue;
        }
        SetValue(startValue);
        barNameText.text = barName;
    }

    public void SetBarName(string text)
    {
        barName = text;
        barNameText.text = text;
    }

    public void SetValue(float newValue)
    {
        if(newValue < minValue)        {
            currentValue = minValue;
            onMinimumReached.Invoke();
        }
        else if (newValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = newValue;
        }
        RenderValue();
    }

    public void DescreaseValue(float decrement)
    {
        if (currentValue - decrement < minValue)
        {
            currentValue = minValue;
            onMinimumReached.Invoke();
        }
        else
        {
            currentValue -= decrement;

        }
        RenderValue();
    }

    public void IncreaseValue(float incremet)
    {
        if (currentValue + incremet > maxValue)
        {
            currentValue = maxValue;
        } else
        {
            currentValue += incremet;
        }
        RenderValue();
    }

    private void RenderValue()
    {
        float fillAmount = currentValue / maxValue;
        fillAmount = fillAmount > 1f ? 1f : fillAmount;
        fillAmount = fillAmount < 0f ? 0f : fillAmount;
        barValueImage.fillAmount = fillAmount;
        onValueChanged.Invoke();
    }

}

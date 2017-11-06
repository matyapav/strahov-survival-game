using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CurrencyController : MonoBehaviourSingleton<CurrencyController> {

    public Text currencyText;

    [System.Serializable]
    public class ValueEvent : UnityEvent { }

    [Tooltip("Called after rendering new value")]
    public ValueEvent onValueChanged;

    public delegate void OnMinimumReached();
    public OnMinimumReached onMinimumReached;

    public float startValue = 1500f;
    public float minValue = 0f;
    public float maxValue = 999999f;

    private float currentValue;

    private void Start()
    {
        //set start value into current value
        
        if (startValue < minValue)
        {
            currentValue = minValue;
        }
        else if(startValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = startValue;
        }
        SetCurrencyText();
    }

    public bool SetValue(float newValue)
    {
        if (newValue > maxValue)
        {
            currentValue = maxValue;
            OnValueChanged(false);
            return true;
        }
        if (newValue <= minValue)
        {
            return false;
        }
        else
        {
            currentValue = newValue;
        }
        OnValueChanged(false);
        return true;
    }

    public bool DescreaseValue(float decrement)
    {
        if (currentValue - decrement <= minValue)
        {
            return false;
        }
        else
        {
            currentValue -= decrement;
        }
        OnValueChanged(false);
        return true;
    }

    public bool CanDecrease(float decrement)
    {
        if (currentValue - decrement <= minValue)
        {
            return false;
        }
        return true;
    }

    public void IncreaseValue(float incremet)
    {
        if(currentValue + incremet > maxValue)
        {
            currentValue = maxValue;
            OnValueChanged(false);
            return;
        }
        currentValue += incremet;
        OnValueChanged(false);
    }


    private void OnValueChanged(bool minimumReached)
    {
        SetCurrencyText();
        onValueChanged.Invoke();
        if (minimumReached)
        {
            onMinimumReached.Invoke();
        }
    }
    
    private void SetCurrencyText()
    {
        if (currencyText != null)
        {
            if (currentValue > 10000)
            {
                currencyText.text = (currentValue / 1000 + "K");
            }
            else
            {
                currencyText.text = currentValue.ToString();
            }
        }
    }
}

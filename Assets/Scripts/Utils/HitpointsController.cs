using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitpointsController : MonoBehaviour {

    public BarController hpBar;

    [System.Serializable]
    public class ValueEvent : UnityEvent { }

    [Tooltip("Called after rendering new value into bar")]
    public ValueEvent onValueChanged;

    public delegate void OnMinimumReached();
    public OnMinimumReached onMinimumReached;

    public float startValue = 100f;
    public bool startAtMaximum = true;


    public float maxValue = 100f;
    public float minValue = 0f;

    private float currentValue;

    private void Start()
    {
        //setup bar controller
        if(hpBar != null)
        { 
            hpBar.SetMaxValue(maxValue);
            hpBar.SetMinValue(minValue);
        }

        if (startAtMaximum)
        {
            startValue = maxValue;
        }
        //set start value into current value
        if (startValue < minValue)
        {
            currentValue = minValue;
        }
        else if (startValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = startValue;
        }
    }

    public void SetValue(float newValue)
    {
        if (newValue <= minValue)
        {
            currentValue = minValue;
            OnValueChanged(true);
            return;
        }
        else if (newValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = newValue;
        }
        OnValueChanged(false);
    }

    public void DescreaseValue(float decrement)
    {
        if (currentValue - decrement <= minValue)
        {
            currentValue = minValue;
            OnValueChanged(true);
            return;
        }
        else
        {
            currentValue -= decrement;
        }
        if(hpBar != null) { 
             hpBar.EnableBar(GetComponent<Blok>().name + " is under attack!");
        }
        OnValueChanged(false);
    }

    public void IncreaseValue(float incremet)
    {
        if (currentValue + incremet >= maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue += incremet;
        }
        OnValueChanged(false);
    }

    private void OnValueChanged(bool minimumReached)
    {
        if(hpBar != null)
        { 
           
            hpBar.SetValue(currentValue);
        }
        onValueChanged.Invoke();
        if (minimumReached)
        {
            onMinimumReached.Invoke();
            if(hpBar != null)
            { 
                hpBar.DisableBar();
                hpBar = null;
            }
        }
    }


    public bool CanIncrease(float amount)
    {
        return currentValue + amount <= maxValue;
    }
}

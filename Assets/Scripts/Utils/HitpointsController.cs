﻿using System;
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

    public bool minimumReached = false;

    private bool isUnderAttack = false;
    public bool IsUnderAttack
    {
        get
        {
            return isUnderAttack;
        }
    }

    // TODO: Made public for debug
    public float currentValue;

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
            minimumReached = true;
            OnValueChanged();
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
        OnValueChanged();
    }

    public void DescreaseValue(float decrement)
    {
        CancelInvoke("isNoMoreUnderAttack");
        if (currentValue - decrement <= minValue)
        {
            currentValue = minValue;
            minimumReached = true;
            OnValueChanged();
            return;
        }
        else
        {
            currentValue -= decrement;
        }
        if(hpBar != null) { 
             hpBar.EnableBar(GetComponent<Blok>().name + " is under attack!");
        }
        isUnderAttack = true;
        Invoke("isNoMoreUnderAttack", 5f);
        OnValueChanged();
    }

    private void isNoMoreUnderAttack()
    {
        isUnderAttack = false;
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
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        if (hpBar != null)
        { 
            hpBar.SetValue(currentValue);
        }
        onValueChanged.Invoke();
        if (minimumReached)
        {
            CancelInvoke("isNoMoreUnderAttack");
            isUnderAttack = false;
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

    public float GetCurrentValue() {
        return currentValue;
    }
}

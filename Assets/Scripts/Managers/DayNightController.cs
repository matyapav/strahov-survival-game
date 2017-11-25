using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviourSingleton<DayNightController> {

    public Animator lightAnimator;
    public GameObject lamps;
    public Text dayNightText;
    private DayNightPhase phase;
    
    private int dayCounter = 1;

    public DayNightPhase Phase
    {
        get
        {
            return phase;
        }
    }

    public void Start()
    {
        phase = DayNightPhase.DAY;
        dayNightText.text = "Day "+ dayCounter;
    }

    public void SwitchPhace()
    {
        lightAnimator.SetBool("night", !lightAnimator.GetBool("night"));
        if(phase == DayNightPhase.DAY)
        {
            phase = DayNightPhase.NIGHT;
            MainUISoundManager.Instance.PlayNight();
            dayNightText.text = "Night " + dayCounter;
            SetLampsOn(true);
        }
        else if(phase == DayNightPhase.NIGHT)
        {
            dayCounter++;
            phase = DayNightPhase.DAY;
            MainUISoundManager.Instance.PlayDay();
            dayNightText.text = "Day " + dayCounter;
            SetLampsOn(false);
           
        }
    }

    private void SetLampsOn(bool lampsOn)
    {
        foreach (Animator lampAnimator in lamps.transform.GetComponentsInChildren<Animator>(true))
        {
            lampAnimator.SetBool("on", lampsOn);
        }
    }

    public bool IsDay
    {
        get
        {
            return phase == DayNightPhase.DAY;
        }
    }

    public bool IsNight
    {
        get
        {
            return phase == DayNightPhase.NIGHT;
        }
    }

}

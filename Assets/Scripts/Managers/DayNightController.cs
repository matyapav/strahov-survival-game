using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviourSingleton<DayNightController> {

    public Animator lightAnimator;
    private DayNightPhase phase;

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
    }

    public void SwitchPhace()
    {
        lightAnimator.SetBool("night", !lightAnimator.GetBool("night"));
        if(phase == DayNightPhase.DAY)
        {
            phase = DayNightPhase.NIGHT;
        }
        else if(phase == DayNightPhase.NIGHT)
        {
            phase = DayNightPhase.DAY;
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

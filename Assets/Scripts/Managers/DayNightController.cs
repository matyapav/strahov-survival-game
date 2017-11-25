using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviourSingleton<DayNightController> {

    public Text dayNightText;
    private DayNightPhase phase;
    
    private int dayCounter = 0;

    public DayNightPhase Phase
    {
        get
        {
            return phase;
        }
    }

    public void Start()
    {
        StartDayPhase();
    }

    public void SwitchPhace()
    {
        if(phase == DayNightPhase.DAY)
        {
            StartNightPhase();
        }
        else if(phase == DayNightPhase.NIGHT)
        {
            StartDayPhase();
        }
    }

    private void StartNightPhase()
    {
        LightsController.Instance.SetDirectionalLightOn(false);
        phase = DayNightPhase.NIGHT;
        MainUISoundManager.Instance.PlayMain();
        MainUISoundManager.Instance.PlayNight();
        dayNightText.text = "Night " + dayCounter;
        LightsController.Instance.SetLampsOn(true);
        MainCanvasManager.Instance.HideBuildMenu();
    }

    private void StartDayPhase()
    {
        LightsController.Instance.SetDirectionalLightOn(true);
        dayCounter++;
        phase = DayNightPhase.DAY;
        MainUISoundManager.Instance.PlayDay();
        dayNightText.text = "Day " + dayCounter;
        LightsController.Instance.SetLampsOn(false);
        MainCanvasManager.Instance.ShowBuildMenu();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviourSingleton<LightsController>
{

    public Animator lightAnimator;
    public GameObject lamps;

    
    public void SetDirectionalLightOn (bool directionalLightOn) {
        lightAnimator.SetBool("night", !directionalLightOn);
    }

    public void SetLampsOn(bool lampsOn)
    {
        foreach (Animator lampAnimator in lamps.transform.GetComponentsInChildren<Animator>(true))
        {
            lampAnimator.SetBool("on", lampsOn);
        }
    }
}

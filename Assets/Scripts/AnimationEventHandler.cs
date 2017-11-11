using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour {

    [System.Serializable]
    public class AnimationEvent : UnityEvent { };

    public AnimationEvent onAnimationStart;
    public AnimationEvent onAnimationEnd;

    public void OnAnimationStart()
    {
        onAnimationStart.Invoke();
    }

    public void OnAnimationEnd()
    {
        onAnimationEnd.Invoke();
    }
}

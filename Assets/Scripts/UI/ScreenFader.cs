using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenFader : MonoBehaviourSingleton<ScreenFader> {

	private const string ANIM_FADE_OUT = "fadeOut";
	private const string ANIM_FADE_IN = "fadeIn";

	[HideInInspector]
	public UnityEvent onFadeOut;
	[HideInInspector]
	public UnityEvent onFadeIn;

	void Start()
	{
		fadeAnimator = GetComponent<Animator>();
	}
	private Animator fadeAnimator;
	public void FadeOut(){
		fadeAnimator.SetTrigger(ANIM_FADE_OUT);
	}

	public void FadeIn(){
		fadeAnimator.SetTrigger(ANIM_FADE_IN);
	}
	public void OnFadeIn(){
		onFadeIn.Invoke();
	}

	public void OnFadeOut(){
		onFadeOut.Invoke();
	}
}
	
	

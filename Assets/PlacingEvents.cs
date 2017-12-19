using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacingEvents : MonoBehaviour {

	[System.Serializable]
	public class PlacingEvent : UnityEvent {};

	public PlacingEvent onBeginPlacing;
	public PlacingEvent onPlaced;

	private bool placing;
	private bool placed;

	public void InvokeOnBeginPlacing(){
		if(onBeginPlacing != null && !placing){
			onBeginPlacing.Invoke();
			placing = true;
			placed = false;
		}
	}

	public void InvokeOnPlaced(){
		if(onPlaced != null && placing && !placed){
			onPlaced.Invoke();
			placing = false;
			placed = true;
		}
	}

}

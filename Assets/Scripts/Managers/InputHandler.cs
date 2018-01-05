using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO predelat sem veskere controls mimo UI (kamera taky???)
public class InputHandler : MonoBehaviourSingleton<InputHandler> {

    public UnityEvent Controls_Build;
    public UnityEvent Controls_Exit;
    public UnityEvent Controls_Agree;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            if (DayNightController.Instance.IsDay) {
                // Controls_Build.Invoke();
                BuildMenuController.Instance.Toggle();
            }
        } 
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Controls_Exit.Invoke();
        } 
        if (Input.GetKeyDown(KeyCode.Return)) {
            Controls_Agree.Invoke();
        } 
	}

}

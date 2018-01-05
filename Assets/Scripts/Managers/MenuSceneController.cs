using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Start with the camera animation on scene start
	    Camera.main.GetComponent<Animator>().Play("CameraStart");	
        Time.timeScale = 1;
	}

}

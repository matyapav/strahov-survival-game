using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO predelat sem veskere controls mimo UI (kamera taky???)
public class InputHandler : MonoBehaviourSingleton<InputHandler> {

    public BuildMenuController buildMenuController;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildMenuController.Toggle();
        }
	}

}

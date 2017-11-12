using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarket : MonoBehaviour {

    private void Update () {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f)) 
            {
                // If i really hit this (tag BlackMarket must be set)
                if (hit.collider.gameObject.tag == gameObject.tag) {
                    MainEventManager.Instance.BlackMarketMenuOpen.Invoke();
                }
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BlackMarket : MonoBehaviour {

    [Tooltip("The TAG of the BlackMarket")]
    public string Tag;

    private void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f)) 
            {
                // If I really hit this (tag BlackMarket must be set)
                if (hit.collider.gameObject.tag == Tag) {
                    // If I hit the blackMarket Invoke the event
                    MainEventManager.Instance.OnBlackMarketClicked.Invoke();
                    MainUISoundManager.Instance.PlayBlop();
                }
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasloController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Add máslo
        if(other.tag == "Player") {
            Destroy(gameObject);
        }
    }
}

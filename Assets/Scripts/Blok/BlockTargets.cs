using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTargets : MonoBehaviour {

    public List<Transform> targets;

    private void Start()
    {
        // Add all targets to the target
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).tag == "Target") {
               targets.Add(transform.GetChild(i).transform); 
            }
        }
    }

    // Get a random target
    public Transform GetRandomTarget() {
        int random = Random.Range(0, targets.Count);
        return targets[random].transform;
    }
}

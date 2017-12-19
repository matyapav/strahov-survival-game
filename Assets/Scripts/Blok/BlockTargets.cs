using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTargets : MonoBehaviour {

    public List<Transform> targets;

    // Get a random target
    public Transform GetRandomTarget() {
        int random = Random.Range(0, targets.Count);
        return targets[random].transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main object manager holds an reference to all active objects in the scene
public class MainObjectManager : MonoBehaviourSingleton<MainObjectManager> {

    [Header("All the spawned objects in the scene are stored in this script")]
    // Arary containing all blocks as GameObjects
    public List<GameObject> bloky;

    // List that contains all the zombies in the scene
    public List<GameObject> zombies;

    // Returns a random blok from the array
    public GameObject GetRandomBlock() {
        if (bloky.Count < 1) {
            return null;
        }

        return bloky[Random.Range(0, bloky.Count)];
    }
}

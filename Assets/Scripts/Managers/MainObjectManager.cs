using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectManager : MonoBehaviourSingleton<MainObjectManager> {

    [Header("All the spawned objects in the scene are stored in this script")]
    // Arary containing all blocks as GameObjects
    public GameObject[] bloky;

    // List that contains all the zombies in the scene
    public List<GameObject> zombies;


}

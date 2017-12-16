using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main object manager holds an reference to all active objects in the scene
public class MainObjectManager : MonoBehaviourSingleton<MainObjectManager> {

    [Header("All the spawned objects in the scene are stored in this script")]
    public List<GameObject> bloky;
    public List<GameObject> zombies;

    public GameObject player;

    // The objects that need to be hidden / shown 
    public GameObject[] DayObjects;
    public GameObject[] NightObjects;

    // Returns a random blok from the array
    public GameObject GetRandomBlock()
    {
        if (bloky.Count < 1)
        {
            return null;
        }
        return bloky[Random.Range(0, bloky.Count)];
    }

    // TODO: Attach this to an event?
    // Update all the objects that need to be hidden or shown
    public void UpdateSwitchableObjects(DayNightPhase nextPhase) {
        // Switching to day
        if (nextPhase == DayNightPhase.DAY)
        {
            SetStateToArray(ref NightObjects, false);   // Deactivate night
            SetStateToArray(ref DayObjects, true);      // Activate day

            // deactivate the player
            if (player != null) player.SetActive(false);
        }
        // Switching to night
        else if(nextPhase == DayNightPhase.NIGHT) {     
            SetStateToArray(ref NightObjects, true);    // Activate night
            SetStateToArray(ref DayObjects, false);     // Deactivate day

            // activate the player
            if (player != null) player.SetActive(true);
        }
    }

    // Kill all zombies in the scene
    public void KillAllZombiesInScene() {
        int count = zombies.Count;
        List<ZombieHealth> zombieHealths = new List<ZombieHealth>();

        // First get all the zombies
        for (int i = 0; i < count; i++) {
            ZombieHealth zhl = zombies[i].GetComponent<ZombieHealth>();
            zombieHealths.Add(zhl);
        }

        // Then kill em all
        for (int i = 0; i < count; i++) {
            zombieHealths[i].Damage(zombieHealths[i].health * 2);
        }
    }

    // Sets state to all objects in the array
    private static void SetStateToArray(ref GameObject[] array, bool newState) {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] != null) {
                array[i].SetActive(newState);
            }
        }
    } 
}

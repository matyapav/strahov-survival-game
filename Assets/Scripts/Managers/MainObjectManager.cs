using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main object manager holds an reference to all active objects in the scene
public class MainObjectManager : MonoBehaviourSingleton<MainObjectManager> {

    [Header("All the spawned objects in the scene are stored in this script")]
    public List<GameObject> bloky;

    // List of key-value pairs that contain the info about the zombie's wave
    // - int - the id of wave
    // - list<GameObject> list of zombies in the wave
    public List<KeyValuePair<int, List<GameObject>>> zombies_wawes;

    // the player's gameobject that gets activated or deactivated at day/night
    public GameObject player;

    // The objects that need to be hidden / shown 
    public GameObject[] DayObjects;
    public GameObject[] NightObjects;

    // init zombies-waves
    private void Start() {
        zombies_wawes = new List<KeyValuePair<int, List<GameObject>>>();
    }

    // Exit the game if there is no block to be attacked
    private void LateUpdate() {
        if (GetRandomActiveBlock() == null) {
            MainCanvasManager.Instance.ShowGameOver();
        }
    }

    // Returns a random blok from the array
    public GameObject GetRandomActiveBlock()
    {
        List<GameObject> active_blocks = new List<GameObject>();

        foreach (var b in bloky) {
            Blok blok = b.GetComponent<Blok>();

            if(b.activeSelf && !blok.Dead()) {
                active_blocks.Add(b);
            }
        }

        if (active_blocks.Count < 1) {
            return null;
        }

        return active_blocks[Random.Range(0, active_blocks.Count)];
    }

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

    // Kills all zombies in all waves in the scene. Also remowes the waves.
    public void KillAllZombiesInScene() {
        // Get all KVPs first
        List<KeyValuePair<int, List<GameObject>>> kvps = new List<KeyValuePair<int, List<GameObject>>>();
        foreach (KeyValuePair<int, List<GameObject>> KVP in zombies_wawes) {
            kvps.Add(KVP);
        }

        // Then remove them one by one
        int wave_count = kvps.Count;
        for (int i = 0; i < wave_count; i++) {
            DestroyWave(kvps[i].Key);
        }
    }

    // Kill all zombies in a wave and destroy it
    public void DestroyWave (int wave_id) {
        var list = GetWaveListByID(wave_id);

        // Return if the wave does not exist
        if (list == null) 
            return;

        // Else kill all zombies in the wave
        // First obtain the zombies helth scripts
        List<ZombieHealth> zombieHealths = new List<ZombieHealth>();
        foreach (GameObject zomb in list)
        {
            ZombieHealth zhl = zomb.GetComponent<ZombieHealth>();
            zombieHealths.Add(zhl);
        }

        // Then kill all the zombies
        foreach (var zhl in zombieHealths)
        {
            zhl.Damage(zhl.health * 2);
        }

        // Then remove the whole KVP from the kvp array
        zombies_wawes.Remove(new KeyValuePair<int, List<GameObject>>(wave_id, list));
    }

    // Get the wave list of zombies by their ID
    public List<GameObject> GetWaveListByID(int wave_id) {
        foreach (var kvp in zombies_wawes) {
            if (kvp.Key == wave_id) {
                return kvp.Value;
            }
        }
        return null;
    }

    // Add a zombie to a wave with designated ID
    public void AddZombieToWave (GameObject zombie, int wave_id) {
        List <GameObject> wlist = GetWaveListByID(wave_id);

        if (wlist != null) {
            wlist.Add(zombie);    
        } else if (wlist == null) {
            // create a new entry with this ID
            KeyValuePair<int, List<GameObject>> wavekvp = new KeyValuePair<int, List<GameObject>>(wave_id, new List<GameObject>());
            wavekvp.Value.Add(zombie);

            // add the kvp into the zombie_wave kvp array
            zombies_wawes.Add(wavekvp);
        }
    }

    // Remove a zombie from the DB
    public void RemoveZombie (GameObject zombie) {
        bool deleteWave = false;
        int deleteWaveID = -1;

        foreach(var _kvp in zombies_wawes) {
            if (_kvp.Value.Contains(zombie)) {
                _kvp.Value.Remove(zombie);

                if (_kvp.Value.Count == 0) {
                    deleteWave = true;
                    deleteWaveID = _kvp.Key;
                }

                break;
            }
        }

        // If the wave was empty, then delete it
        if(deleteWave) {
            DestroyWave(deleteWaveID);
        }
    }

	// Get all zombies in the scene. Not very effective tho. Should not be used alot.
    public List<GameObject> GetAllZombies() {
        List<GameObject> all_zombies = new List<GameObject>();
        foreach(var kvp in zombies_wawes) {
            foreach(var zomb in kvp.Value) {
                all_zombies.Add(zomb);
            }
        }
        return all_zombies;
    }

    // Count all the zombie GameObjects that are stored in the zombie_waves
    public int CountZombiesInScene()
    {
        int count = 0;

        foreach (var kvp in zombies_wawes) {
            if(kvp.Value != null ) {
                count += kvp.Value.Count;
            }
        }

        return count;
    }

    // Get all the ids of the acrive waves in the scene
    public List<int> GetCurrentlyActiveWavesInScene() {
        List<int> wave_id_list = new List<int>();

        foreach (var kvp in zombies_wawes) {
            wave_id_list.Add(kvp.Key);
        }

        return wave_id_list;
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

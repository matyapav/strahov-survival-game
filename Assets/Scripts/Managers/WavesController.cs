using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesController : MonoBehaviourSingleton<WavesController>
{
    [Header("Wave spawning")]
    public BusSpawner[] buses;

    [Header("User interface")]
    public GameObject waveUI;
    public Text waveHp1, waveHp2;
    public Text waveZombiesCount;
    public Text waveNumber;
    public Image waweHpImage;

    private int actualBusIndex = 0;
    private bool canSpawnNewWave = true;
    private int actualWaveZombieCount;
    private float actualWaveHp;
    private float actualWaveHpBackup;
    private int waveIndex = 1;
    private int numberOfWaves;

    private int wave_busses_left = 0;
    private int wave_busses_dipatched = 0;
    private int wave_busses_target = 0;

    private void OnEnable()
    {
        MainEventManager.Instance.OnBusLeaving.AddListener(IncreaseBussesLeft);
        MainEventManager.Instance.OnBusDispatched.AddListener(IncreaseBussesDispatched);

        MainEventManager.Instance.OnBusLeaving.AddListener(AllowSpawningNewWave);
    }

    void IncreaseBussesDispatched() {
        wave_busses_dipatched++;
    }

    void IncreaseBussesLeft() {
        wave_busses_left++;
    }


    private void Start()
    {
        actualWaveZombieCount = MainObjectManager.Instance.CountZombiesInScene();
        foreach (GameObject zombie in MainObjectManager.Instance.GetAllZombies()){
            actualWaveHp += zombie.GetComponent<ZombieHealth>().health;
        }
    }

    // Spawn a number of waves
    public void SpawnNWaves(int number, int wait_lower, int wait_upper) {
        wave_busses_left = 0;
        wave_busses_dipatched = 0;
        wave_busses_target = number;
        StartCoroutine(SpawnNWavesIE(wait_lower, wait_upper));
    }

    IEnumerator SpawnNWavesIE(int wait_lower, int wait_upper) {

        while (wave_busses_dipatched < wave_busses_target)
        {
            if (canSpawnNewWave)
            {
                if (DayNightController.Instance.Phase == DayNightPhase.DAY) {
                    Debug.LogError("A bus was trying to be spawned during the day");
                    break;
                }

                // Make pause between spawns
                if (wave_busses_dipatched != 0) {
                    yield return new WaitForSeconds(Random.Range(wait_lower, wait_upper));
                }

                StartNextWave();
            }
            // Check every second
            yield return new WaitForSeconds(1);
        }

        // After spaning start trying to switch to day
        while (true) {
            if (wave_busses_left >= wave_busses_target && MainObjectManager.Instance.CountZombiesInScene() == 0) {
                // Some time for player to realise its end of the level
                yield return new WaitForSeconds(5);

                // Switch to day
                MainEventManager.Instance.SwitchDayPhaseEventInvoke();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void StartNextWave()
    {
        if (canSpawnNewWave)
        {   
            buses[actualBusIndex].Arrive();
            actualBusIndex = (actualBusIndex + 1) % buses.Length; // cycle buses
            canSpawnNewWave = false;

            //update wave index
            waveNumber.gameObject.SetActive(true);
            waveNumber.text = "wave "+waveIndex;
            waveIndex++;
        } else
        {
            Debug.LogError("Cannot spawn new wave. Previous wave is still spawning..");
        }
    }

    // Set a flag if the last bus left
    public void AllowSpawningNewWave() {
        canSpawnNewWave = true;
    }

    public void DecreaseWaveHealthAndCount(float amount){
         actualWaveZombieCount--;
        if(actualWaveZombieCount <= 0){
            Debug.Log("Wave finished!"); 
            waveUI.SetActive(false);
        }
        DecreaseWaveHealth(amount);
    }

    public void DecreaseWaveHealth(float amount){
        if(actualWaveHp - amount < 0){
            actualWaveHp = 0f; 
            return;
        }
        actualWaveHp -= amount;
        UpdateWaveUIInfo();
    }

    public void IncreaseWaveCountAndHp(float hp) {
        if(!waveUI.activeInHierarchy){
            waveUI.SetActive(true);
        }
        actualWaveHp += hp;
        actualWaveZombieCount ++;
        actualWaveHpBackup += hp;
        UpdateWaveUIInfo();
    }

    private void UpdateWaveUIInfo(){
        waweHpImage.fillAmount = actualWaveHp/actualWaveHpBackup;
        waveZombiesCount.text = "Students "+actualWaveZombieCount+" / " +actualWaveZombieCount;
        waveHp1.text = waveHp2.text = "Hp: "+actualWaveHp+" / "+actualWaveHpBackup;
    }
}

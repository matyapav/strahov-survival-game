using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesController : MonoBehaviourSingleton<WavesController> {
    [Header("Wave spawning")]
    public BusSpawner[] buses;
    public int numberOfWavesPerNight = 3;
    public float secondsBetweenSpawns = 90f;

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

    private void Start()
    {
        // For each bus add a listener to the MainEventManager
        foreach (BusSpawner spawner in buses) {
            MainEventManager.Instance.OnBusLeaving.AddListener(AllowSpawningNewWave);
        }

        actualWaveZombieCount = MainObjectManager.Instance.CountZombiesInScene();
        foreach(GameObject zombie in MainObjectManager.Instance.GetAllZombies()){
            actualWaveHp += zombie.GetComponent<ZombieHealth>().health;
        }
    }

    public void StartNextWave()
    {
        if (canSpawnNewWave)
        {   
            buses[actualBusIndex].Arrive();
            actualBusIndex = (actualBusIndex + 1) % buses.Length; //cycle buses
            canSpawnNewWave = false;

            //update wave index
            waveNumber.gameObject.SetActive(true);
            waveNumber.text = "wave "+waveIndex;
            waveIndex++;
        } else
        {
            Debug.Log("Cannot spawn new wave. Previous wave is still spawning..");
        }
    }

    public void AllowSpawningNewWave() {
        canSpawnNewWave = true;
        if(waveIndex <= numberOfWavesPerNight){
            Invoke("StartNextWave", secondsBetweenSpawns);
        }
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

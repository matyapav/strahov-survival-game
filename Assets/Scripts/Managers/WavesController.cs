using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesController : MonoBehaviourSingleton<WavesController> {

    public BusSpawner[] buses;

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
            //handle wave UI
            waveUI.SetActive(true);
            actualWaveZombieCount += buses[actualBusIndex].numberOfZombies;
            actualWaveHp += actualWaveZombieCount*buses[actualBusIndex].ZombiePrefab.GetComponent<ZombieHealth>().health; //TODO az bude vice zombie v autobuse potreba vymyslet jinak!!!!!
            actualWaveHpBackup = actualWaveHp;
            UpdateWaveUIInfo();

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
    }

    public void DecreaseWaveHealth(float amount){
        if(actualWaveHp - amount < 0){
            actualWaveHp = 0f; 
            return;
        }
        actualWaveHp -= amount;
        UpdateWaveUIInfo();
    }

    public void DecreaseWaveCount(int amount){
        if(actualWaveZombieCount - amount < 0){
            actualWaveZombieCount = 0;
            Debug.Log("Wave finished!"); 
            return;
        }
        actualWaveZombieCount -= amount;
        UpdateWaveUIInfo();
    }

    private void UpdateWaveUIInfo(){
        waweHpImage.fillAmount = actualWaveHp/actualWaveHpBackup;
        waveZombiesCount.text = "Zombies "+actualWaveZombieCount+" / " +actualWaveZombieCount;
        waveHp1.text = waveHp2.text = "Hp: "+actualWaveHp+" / "+actualWaveHp;
    }
}

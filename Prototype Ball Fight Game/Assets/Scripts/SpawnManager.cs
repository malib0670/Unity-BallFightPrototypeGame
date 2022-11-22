using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    public float spawnRange = 9;
    public int enemyCount; 
    public int waveNumber = 1; 

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnStart(waveNumber);
        powerupSpawnStart();
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnUpdate();
    }

    public void enemySpawnStart(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, generateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    public void powerupSpawnStart() 
    {
        Instantiate(powerupPrefab, generateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    public void enemySpawnUpdate()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; 
                                                        
        if (enemyCount == 0)
        {
            waveNumber++;
            enemySpawnStart(waveNumber); 
            Instantiate(powerupPrefab, generateSpawnPosition(), powerupPrefab.transform.rotation); 
        }                                                                                           
    }

    public Vector3 generateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}

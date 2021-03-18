using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public int SpawnTimer;
    public int SpawnPointSelector;
    public int SpawnPointMax;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60; //limits frame rate to 60
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnTimer >= 360)
        {
            SpawnEnemy();
        }
        SpawnTimer++;
    }

    void SpawnEnemy() //handles spawn point selection and spawning of enemies
    {
        SpawnPointMax = SpawnPoints.Length;
        SpawnPointSelector = Random.Range(0, SpawnPointMax);
        GameObject ChosenSpawn = SpawnPoints[SpawnPointSelector];
        Vector3 SpawnPos = ChosenSpawn.transform.position;
        GameObject SpawnedEnemy;
        SpawnedEnemy = Instantiate(Enemy, SpawnPos, transform.rotation);
        Debug.Log("Spawned enemy.");
        SpawnTimer = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // enemy prefab
    public GameObject enemy;
    // spawn cooldown time constant
    public int mCoolDown;

    private GameObject[] spawnPoints;
    private bool spawn;
    // current cooldown 
    private float coolDown;

    void Start()
    {
        spawn = true;

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            // spawn an fixed point enemy
            Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            // spawn enemy
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = spawnPos;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
            if(list.Length>0)
            {
                list[0].GetComponent<Enemy>().Kill();
            }
        }

        coolDown -= Time.deltaTime;
        if(coolDown <= 0)
        {
            coolDown = 0;
        }

        if(GameObject.FindGameObjectsWithTag("Enemy").Length>=4)
        {
            spawn = false;
        }
        else
        {
            spawn = true;
        }

        if(spawn && coolDown == 0)
        {
            SpawnEnemy();
            coolDown = mCoolDown;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        // spawn enemy
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPos;
    }
}

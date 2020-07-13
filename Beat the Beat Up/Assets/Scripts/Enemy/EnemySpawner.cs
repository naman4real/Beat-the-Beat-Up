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

    private bool spawn;
    // current cooldown 
    private float coolDown;

    void Start()
    {
        spawn = true;
    }

    void Update()
    {
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
        // spawn enemy
        GameObject newEnemy = Instantiate(enemy);
    }
}

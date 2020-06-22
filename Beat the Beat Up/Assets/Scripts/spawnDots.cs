using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDots : MonoBehaviour
{
    [SerializeField] private GameObject[] dots;
    //Spawn this object
    public GameObject spawnObject;

    public float maxTime = 5;
    public float minTime = 2;

    //current time
    public static float time;

    //The time to spawn the object
    public static bool spawn = true;
    private float spawnTime;
    public static GameObject go;

    void Start()
    {
        //SetRandomTime();
        //time = minTime;
        time = 0;
    }

    void FixedUpdate()
    {

        //Counts up

        //time += Time.deltaTime;

        ////Check if its the right time to spawn the object
        //if (time >= spawnTime)
        //{
        //    SpawnObject();
        //    SetRandomTime();
        //}

        time += Time.deltaTime;
        if (spawn)
        {
            
            SpawnObject();
        }
 

    }


    //Spawns the object and resets the time
    void SpawnObject()
    {
        if (go && go.activeInHierarchy)
            go.SetActive(false);
        //time = minTime;
        //location = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
        //go =Instantiate(spawnObject, location, spawnObject.transform.rotation);
        go=dots[Random.Range(0, dots.Length)];
        go.SetActive(true);
        spawn = false;
        
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}
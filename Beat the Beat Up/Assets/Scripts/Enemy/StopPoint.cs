using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    private bool taken;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        taken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsTaken()
    {
        return taken;
    }

    public void Take(GameObject pEnemy)
    {
        taken = true;
        enemy = pEnemy;
    }

    public void Untake()
    {
        taken = false;
        enemy = null;
    }
}

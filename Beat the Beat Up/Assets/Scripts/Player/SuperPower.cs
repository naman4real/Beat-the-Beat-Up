using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : MonoBehaviour
{
    public TimeManager timeManager;
    void Update()
    {
        if (Input.GetKeyDown("1"))
            SlowDown();
    }
    void SlowDown()
    {
        timeManager.SlowMotion();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grab : MonoBehaviour
{

    bool start = false;
    [SerializeField] GameObject enemy;
    void Update()
    {
        if (OVRGrabber.grabbed && !start)
        {
            enemy.GetComponent<PartDots>().grab();
            start = true;
        }
        else if (!OVRGrabber.grabbed && start)
        {
            enemy.GetComponent<PartDots>().release();
            start = false;
        }

    }

}

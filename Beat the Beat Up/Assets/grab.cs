using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{

    bool start = false;
    void Update()
    {
        if (OVRGrabber.grabbed && !start)
        {
            GameObject.Find("Enemy1").GetComponent<PartDots>().grab();
            start = true;
        }
        else if (!OVRGrabber.grabbed && start)
        {
            GameObject.Find("Enemy1").GetComponent<PartDots>().release();
            start = false;
        }

    }

}

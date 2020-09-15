using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private float activeTime;
    Color color;
    GameObject go;

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.name == "Right Stomach" && gameObject.GetComponentInParent<Parts>().gameObject.name == "Enemy1")
        //    Debug.Log("deltatime " + activeTime);

        if(activeTime>0)
        {
            activeTime -= Time.deltaTime;

            if(activeTime <= 0)
            {
                go.SetActive(false);
                activeTime = 0;
            }
        }
    }

    public void ActivateDot(float spanSec, string attack)
    {
        go = transform.Find(attack).gameObject;
        go.SetActive(true);
        //Debug.Log("Setting ActiveTime to " + spanSec);
        activeTime = spanSec;
    }
}

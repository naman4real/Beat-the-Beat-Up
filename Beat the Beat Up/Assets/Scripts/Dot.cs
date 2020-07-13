using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private float activeTime;
    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Right Stomach" && gameObject.GetComponentInParent<Parts>().gameObject.name == "Enemy1")
            Debug.Log("deltatime " + activeTime);
        if(activeTime>0)
        {
            activeTime -= Time.deltaTime;

            if(activeTime <= 0)
            {
                activeTime = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void ActivateDot(float spanSec)
    {
        gameObject.SetActive(true);
        Debug.Log("Setting ActiveTime to " + spanSec);
        activeTime = spanSec;
    }
}

using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            var col = gameObject.GetComponent<Renderer>().material.color;
            col.a = 0f;
            gameObject.GetComponent<Renderer>().material.color = col;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            var col = gameObject.GetComponent<Renderer>().material.color;
            col.a = 1f;
            gameObject.GetComponent<Renderer>().material.color = col;
        }
    }
}

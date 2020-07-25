using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemy1,enemy2,enemy3,enemy4;
    Rigidbody rb;
    
    void Start()
    {
        rb = enemy1.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            enemy1.GetComponent<Animator>().enabled = false;
            rb.isKinematic = false;

            rb.AddForce((1.2f*enemy1.transform.up - enemy1.transform.forward)* 200f,ForceMode.Impulse);

            StartCoroutine(stopForce());
        }
    }
    IEnumerator stopForce()
    {
        yield return new WaitForSeconds(1f);
        
    }
    
}

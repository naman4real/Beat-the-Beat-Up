using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    // Start is called before the first frame update
    bool start = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRGrabber.grabbed)
        {
            //GameObject.Find("Enemy1").GetComponent<PartDots>().grab();
            StartCoroutine(wait());
            start = true;
        }
        //else if (!OVRGrabber.grabbed && start)
        //{
        //    GameObject.Find("Enemy1").GetComponent<PartDots>().release();
        //    start = false;
        //}
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
    //    {
    //        GameObject.Find("Enemy1").GetComponent<PartDots>().grab();
    //    }
    //}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("Enemy1").GetComponent<PartDots>().grab();
    }
}

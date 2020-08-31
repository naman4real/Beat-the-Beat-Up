using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private float time = 0f;    // to prevent accidental collisions
    private int flag = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attackable") && time==0f)
        {
            flag = 1;
            time = 0.4f;
            Debug.Log(collider.name);

            if (collider.gameObject.name == "Right Stomach collider")
                anim.SetTrigger("rightStomach");
            else if (collider.gameObject.name == "Mid Stomach collider")
                anim.SetTrigger("midStomach");
            else if (collider.gameObject.name == "Left Stomach collider")
                anim.SetTrigger("leftStomach");
            else if (collider.gameObject.name == "Right Head collider")
                anim.SetTrigger("rightHead");
            else if (collider.gameObject.name == "Mid Head collider")
                anim.SetTrigger("midHead");
            else if (collider.gameObject.name == "Left Head collider")
                anim.SetTrigger("leftHead");
        }
    }
    private void Update()
    {
        if (flag == 1)
            time -= Time.deltaTime;
        if (time < 0f)
        {
            time = 0f;
            flag = 0;
        }
    }

}



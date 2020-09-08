using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

            if (collider.gameObject.name == "Right Stomach Collider")
                anim.SetTrigger("rightStomach");
            else if (collider.gameObject.name == "Mid Stomach Collider")
                anim.SetTrigger("midStomach");
            else if (collider.gameObject.name == "Left Stomach Collider")
                anim.SetTrigger("leftStomach");
            else if (collider.gameObject.name == "Right Head Collider")
                anim.SetTrigger("rightHead");
            else if (collider.gameObject.name == "Mid Head Collider")
                anim.SetTrigger("midHead");
            else if (collider.gameObject.name == "Left Head Collider")
                anim.SetTrigger("leftHead");

            else if (collider.gameObject.name == "EXPORT_b_l_bicep") //left arm collider
                anim.SetTrigger("leftArmPunch");
            else if (collider.gameObject.name == "EXPORT_b_r_bicep") //right arm collider
                anim.SetTrigger("rightArmPunch");
            else if (collider.gameObject.name == "EXPORT_b_l_wrists")//left hand collider
                anim.SetTrigger("leftHandPunch");
            else if (collider.gameObject.name == "EXPORT_b_r_wrists")//right hand collider
                anim.SetTrigger("rightHandPunch");
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("leftArmPunch");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("rightArmPunch");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("leftHandPunch");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("rightHandPunch");
        }
    }

}



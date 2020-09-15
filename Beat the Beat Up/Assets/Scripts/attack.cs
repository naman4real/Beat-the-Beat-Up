using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class attack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] GameObject go;
    private float time = 0f;// to prevent accidental collisions
    private int flag = 0;
    [SerializeField] Text root;
    private int i = 0;

  
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attackable") && time==0f)
        {
            i += 1;
            flag = 1;
            time = 0.4f;
            Debug.Log(collider.name);

            if (collider.gameObject.name == "Right Stomach Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("rightStomach");
            else if (collider.gameObject.name == "Mid Stomach Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("midStomach");
            else if (collider.gameObject.name == "Left Stomach Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("leftStomach");
            else if (collider.gameObject.name == "Right Head Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("rightHead");
            else if (collider.gameObject.name == "Mid Head Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("midHead");
            else if (collider.gameObject.name == "Left Head Collider")
                collider.transform.root.GetComponent<Animator>().SetTrigger("leftHead");

            else if (collider.gameObject.name == "Chest Collider")//right hand collider
                collider.transform.root.GetComponent<Animator>().SetTrigger("chest");

            else if (collider.gameObject.name == "EXPORT_b_l_bicep") //left arm collider
                collider.transform.root.GetComponent<Animator>().SetTrigger("leftArmPunch");
            else if (collider.gameObject.name == "EXPORT_b_r_bicep") //right arm collider
                collider.transform.root.GetComponent<Animator>().SetTrigger("rightArmPunch");
            else if (collider.gameObject.name == "EXPORT_b_l_wrists")//left hand collider
                collider.transform.root.GetComponent<Animator>().SetTrigger("leftHandPunch");
            else if (collider.gameObject.name == "EXPORT_b_r_wrists")//right hand collider
                collider.transform.root.GetComponent<Animator>().SetTrigger("rightHandPunch");
            root.text = collider.transform.root.ToString() + " " + i.ToString();
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




        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("midHead");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            anim.SetTrigger("leftHead");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("rightHead");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("chest");
        }
    }

}



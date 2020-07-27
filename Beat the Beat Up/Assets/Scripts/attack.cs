using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Attackable"))
        {
            if (collision.collider.gameObject.name == "Right Stomach")
                anim.SetTrigger("rightStomach");
            else if (collision.collider.gameObject.name == "Mid Stomach")
                anim.SetTrigger("midStomach");
            else if (collision.collider.gameObject.name == "Left Stomach")
                anim.SetTrigger("leftStomach");
        }
    }
}

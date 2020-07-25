using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class head : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Text performanceText;
    [SerializeField] private Text timeText;
    private string performance;
    private string time;



    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("hands"))
        {
            var dist = Vector3.Distance(spawnDots.go.transform.position, collision.collider.transform.position);
            anim.SetTrigger("jawHit1");
            BoneHighlighter.bone = GameObject.Find("mixamorig:Head").transform;
            StartCoroutine(wait());


            if (spawnDots.time <= 2f)
                time = "Perfect timing";
            else
                time = "Too Slow";



            if (dist >= 0.3f)
            {
                performance = "Average";

            }
            else if (dist >= 0.2f && dist < 0.3f)
            {
                performance = "Good";

            }
            else
            {
                performance = "Excellent";
            }

            spawnDots.time = 0;
            performanceText.text = performance;
            timeText.text = time;




        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);

        BoneHighlighter.bone = null;
        spawnDots.spawn = true;

    }

}

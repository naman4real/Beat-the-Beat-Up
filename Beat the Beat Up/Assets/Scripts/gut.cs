using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gut : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject tops;

    [SerializeField] private Texture gutTexture;
    [SerializeField] private Texture originalTexture;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("hands"))
        {
            anim.SetTrigger("gutHit");
            tops.GetComponent<SkinnedMeshRenderer>().material.mainTexture = gutTexture;
            StartCoroutine(wait());
        }

        IEnumerator wait()
        {
            yield return new WaitForSeconds(1.5f);

            Debug.Log("tex");
            tops.GetComponent<SkinnedMeshRenderer>().material.mainTexture = originalTexture;
            





        }
    }
}

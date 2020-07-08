using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class isHit : MonoBehaviour
{


    [SerializeField] private Texture gutTexture;
    [SerializeField] private Texture originalTexture;
    [SerializeField] private Camera playerCamera;
    private Animator anim;
    private bool gut, face = false;
     

    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;

    Vector3 dir;
    Vector3 lookSpot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        dir = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetKey(KeyCode.W))
        {
            lookSpot = new Vector3(playerCamera.transform.position.x, 0f, playerCamera.transform.position.z);
            anim.SetBool("run", true);
            transform.position=Vector3.MoveTowards(transform.position, lookSpot,2.5f*Time.deltaTime);
        }
        else
        {           
            anim.SetBool("run", false);
            StartCoroutine(waitToChangeDirction());
        }

        if (Input.GetMouseButtonDown(0))
        {
            face = true;
            anim.SetTrigger("jawHit1");
            BoneHighlighter.bone = GameObject.Find("mixamorig:Head").transform;
            StartCoroutine(wait());

        }
        else if (Input.GetMouseButtonDown(1))
        {
            gut = true;
            anim.SetTrigger("gutHit");
            transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material.mainTexture = gutTexture;
            StartCoroutine(wait());

        }

        transform.forward = (lookSpot - transform.position).normalized;

    }



    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        if (face)
        {
            BoneHighlighter.bone = null;
            face = false;

        }
        else if (gut)
        {
            transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material.mainTexture = originalTexture;
            gut = false;
        }
    }

    IEnumerator waitToChangeDirction()
    {
        yield return new WaitForSeconds(1f);
        lookSpot = new Vector3(playerCamera.transform.position.x - 1f, 0f, playerCamera.transform.position.z);
    }


}

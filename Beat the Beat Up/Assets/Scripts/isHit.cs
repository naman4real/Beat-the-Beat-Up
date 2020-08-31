using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class isHit : MonoBehaviour
{
    [SerializeField] private Texture gutTexture;
    [SerializeField] private Texture originalTexture;
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private Transform root, head, neck, spine, spine2;
    BoneHighlighter boneHighlighter;
    private Animator anim;

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
        boneHighlighter = GetComponent<BoneHighlighter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            lookSpot = new Vector3(playerCamera.transform.position.x, 0f, playerCamera.transform.position.z);
            anim.SetBool("run", true);
            transform.position = Vector3.MoveTowards(transform.position, lookSpot, 2.5f * Time.deltaTime);
        }
        else
        {
            anim.SetBool("run", false);
            StartCoroutine(waitToChangeDirction());
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("jawHit1");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("gutHit");
        }

        //transform.forward = (lookSpot - transform.position).normalized;

    }

    IEnumerator waitToChangeDirction()
    {
        yield return new WaitForSeconds(1f);
        lookSpot = new Vector3(playerCamera.transform.position.x - 1f, 0f, playerCamera.transform.position.z);
    }

    public void log()
    {
        Debug.Log("held");
        anim.enabled = false;
    }

}

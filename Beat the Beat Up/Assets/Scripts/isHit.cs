using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class isHit : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] private Texture gutTexture;
    [SerializeField] private Texture originalTexture;
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private Transform root, head, neck, spine, spine2;
    BoneHighlighter boneHighlighter;
    private Animator anim;
    private bool gut, face = false;
    Renderer bodyRenderer;
    Renderer topRenderer;

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
        topRenderer = transform.Find("Tops").gameObject.GetComponent<Renderer>();
        bodyRenderer = transform.Find("Body").gameObject.GetComponent<Renderer>();
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
            face = true;
            anim.SetTrigger("jawHit1");
            bodyRenderer.material.SetInt("_PartIndex", index);


            StartCoroutine(wait());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            gut = true;
            anim.SetTrigger("gutHit");

            topRenderer.material.SetInt("_PartIndex", index);
            // BoneHighlighter.bone = GameObject.Find("mixamorig:"+ boneToHighlight).transform;
            // boneHighlighter.HighlightWithinDistance(highlightPoint.transform.position, root);


            // transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material.mainTexture = gutTexture;
            StartCoroutine(wait());

        }

        transform.forward = (lookSpot - transform.position).normalized;

    }



    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        if (face)
        {
            bodyRenderer.material.SetInt("_PartIndex", -1);
            BoneHighlighter.bone = null;
            face = false;

        }
        else if (gut)
        {
            topRenderer.material.SetInt("_PartIndex", -1);
            BoneHighlighter.bone = null;
            gut = false;
        }
    }

    IEnumerator waitToChangeDirction()
    {
        yield return new WaitForSeconds(1f);
        lookSpot = new Vector3(playerCamera.transform.position.x - 1f, 0f, playerCamera.transform.position.z);
    }


}

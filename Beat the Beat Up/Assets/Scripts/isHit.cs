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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            face = true;
            //if(Random.value<0.5f)
            //  anim.SetTrigger("jawHit");
            //else
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

    }



    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        //foreach (int i in vertices)
        //{
        //    colors[i].a = Mathf.Max(colors[i].a - Time.deltaTime * 0.5f, 0f);
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


}

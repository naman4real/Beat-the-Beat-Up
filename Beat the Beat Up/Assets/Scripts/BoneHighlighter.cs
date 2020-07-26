using UnityEngine;
using System.Collections;

public class BoneHighlighter : MonoBehaviour
{
	public Color32 highlightColor = Color.red;
	public Color32 regularColor = Color.white;

    Renderer bodyRenderer;
    Renderer topRenderer;

    float bodyTime;
    float topTime;
    
    private void Start()
    {
        topRenderer = transform.Find("Tops").gameObject.GetComponent<Renderer>();
        bodyRenderer = transform.Find("Body").gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        bodyTime -= Time.deltaTime;
        if(bodyTime <= 0)
        {
            bodyRenderer.material.SetInt("_PartIndex", -1);
            bodyTime = 0;
        }

        topTime -= Time.deltaTime;
        if (topTime <= 0)
        {
            topRenderer.material.SetInt("_PartIndex", -1);
            topTime = 0;
        }
    }

    /*-------------------
    0 : right - stomach
    1 : mid - stomach
    2 : left - stomach
    3 : chest
    4 : left - arm
    5 : left - hand
    6 : right - head
    7 : mid - head
    8 : left - head
    9 : right - arm
    10 : right - hand
    11 : default
    -------------------*/

    public void HighlightPart(string part, float span)
    {
        bool body = false, top = false;
        int idx;
        switch(part)
        {
            case "Right Stomach":
                idx = 0;
                top = true;
                break;
            case "Mid Stomach":
                top = true;
                idx = 1;
                break;
            case "Left Stomach":
                top = true;
                idx = 2;
                break;
            case "Chest":
                top = true;
                idx = 3;
                break;
            case "Left Arm":
                top = true;
                body = true;
                idx = 4;
                break;
            case "Left Hand":
                body = true;
                idx = 5;
                break;
            case "Right Head":
                body = true;
                idx = 6;
                break;
            case "Mid Head":
                body = true;
                idx = 7;
                break;
            case "Left Head":
                body = true;
                idx = 8;
                break;
            case "Right Arm":
                body = true;
                top = true;
                idx = 9;
                break;
            case "Right Hand":
                body = true;
                idx = 10;
                break;
            default:
                idx = -1;
                break;
        }
        if(body)
        {
            bodyTime = span;
            bodyRenderer.material.SetInt("_PartIndex", idx);
        }
        if(top)
        {
            topTime = span;
            topRenderer.material.SetInt("_PartIndex", idx);
        }
    }
}

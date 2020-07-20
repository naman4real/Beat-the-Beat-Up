using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    [SerializeField] GameObject RightStomach;
    [SerializeField] GameObject MidStomach;
    [SerializeField] GameObject LeftStomach;
    [SerializeField] GameObject Chest;
    [SerializeField] GameObject LeftArm;
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject RightHead;
    [SerializeField] GameObject MidHead;
    [SerializeField] GameObject LeftHead;
    [SerializeField] GameObject RightArm;
    [SerializeField] GameObject RightHand;
    [SerializeField] Material slapMat, grabMat, punchMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDotAtPart(string part, float span, string attack)
    {
        if (span <= 0) return;

        GameObject toActivate = null;
        switch(part)
        {
            case "Right Stomach":
                toActivate = RightStomach;
                break;
            case "Mid Stomach":
                toActivate = MidStomach;
                break;
            case "Left Stomach":
                toActivate = LeftStomach;
                break;
            case "Chest":
                toActivate = Chest;
                break;
            case "Left Arm":
                toActivate = LeftArm;
                break;
            case "Left Hand":
                toActivate = LeftHand;
                break;
            case "Right Head":
                toActivate = RightHead;
                break;
            case "Mid Head":
                toActivate = MidHead;
                break;
            case "Left Head":
                toActivate = LeftHead;
                break;
            case "Right Arm":
                toActivate = RightArm;
                break;
            case "Right Hand":
                toActivate = RightHand;
                break;
        }

        if(toActivate != null)
        {
            Debug.Log(attack+""+attack.Length);
            if (attack == "Slap")
                toActivate.GetComponent<MeshRenderer>().material = slapMat;
            else if (attack == "Punch")
                toActivate.GetComponent<MeshRenderer>().material = punchMat;
            else if (attack == "Grab")
                toActivate.GetComponent<MeshRenderer>().material = grabMat;
            Debug.Log(toActivate.GetComponent<MeshRenderer>().material.name);
            toActivate.GetComponent<Dot>().ActivateDot(span);
        }
    }
}

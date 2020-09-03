﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.XR;

public class PartDots : MonoBehaviour
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

    [SerializeField] GameObject Spine;    // not bound to any attackable bodypart
    [SerializeField] GameObject LeftArmRagdoll;
    [SerializeField] GameObject RightArmRagdoll;
    [SerializeField] GameObject HeadRagdoll;
    [SerializeField] GameObject LeftHandRagdoll;
    [SerializeField] GameObject RightHandRagdoll;
    [SerializeField] GameObject LeftForearm;    // not bound to any attackable bodypart
    [SerializeField] GameObject RightForearm;   // not bound to any attackable bodypart
    [SerializeField] GameObject Hips;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    GameObject ragdollPart = null;



    GameObject[] bodyParts;
    [SerializeField] GameObject[] Legs;
    GameObject bodyPartToThrow = null;

    Dictionary<GameObject, GameObject> RagdollMapping;


    Rigidbody rbSpine,rbLeftForearm,rbRightForearm;
    int follow = 0;
    // Start is called before the first frame update
    void Start()
    {
        bodyParts = new GameObject[] {RightStomach, MidStomach, LeftStomach, Chest, LeftArm,
                                      LeftHand, RightHead, MidHead, LeftHead, RightArm, RightHand};

        rbSpine = Spine.GetComponent<Rigidbody>();
        rbLeftForearm = LeftForearm.GetComponent<Rigidbody>();
        rbRightForearm = RightForearm.GetComponent<Rigidbody>();
        
        RagdollMapping = new Dictionary<GameObject, GameObject>();
        RagdollMapping.Add(RightStomach, Hips);
        RagdollMapping.Add(MidStomach, Hips);
        RagdollMapping.Add(LeftStomach, Hips);
        RagdollMapping.Add(Chest, Hips);
        RagdollMapping.Add(LeftArm, LeftArmRagdoll);
        RagdollMapping.Add(LeftHand, LeftHandRagdoll);
        RagdollMapping.Add(RightHead, HeadRagdoll);
        RagdollMapping.Add(MidHead, HeadRagdoll);
        RagdollMapping.Add(LeftHead, HeadRagdoll);
        RagdollMapping.Add(RightArm, RightArmRagdoll);
        RagdollMapping.Add(RightHand, RightHandRagdoll);

        //LeftArmRagdoll.transform.parent = gameObject.transform;
        //transform.Find("mixamorig:Hips").parent = LeftArmRagdoll.transform;
    }


    public void ActivateDotAtPart(string part, float span, string attack)
    {
        if (span <= 0) return;

        GameObject toActivate = null;
        switch (part)
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

        if (toActivate != null)
        {
            if (attack == "Slap")
            {
                toActivate.GetComponent<MeshRenderer>().material = slapMat;
            }
            else if (attack == "Punch")
            {
                toActivate.GetComponent<MeshRenderer>().material = punchMat;
            }
            else if (attack == "Grab")
            {
                bodyPartToThrow = toActivate;

                toActivate.GetComponent<MeshRenderer>().material = grabMat;
                rbLeftForearm.isKinematic = false;
                rbRightForearm.isKinematic = false;
                rbSpine.isKinematic = false;
                
                foreach (var bodyPart in bodyParts)
                {
                    if (RagdollMapping[bodyPart] != RagdollMapping[toActivate])
                        RagdollMapping[bodyPart].GetComponent<Rigidbody>().isKinematic = false;
                }


               

                transform.gameObject.GetComponent<Animator>().enabled = false;

            }
            else if (attack == "Throw")
            {
                RagdollMapping[bodyPartToThrow].GetComponent<Rigidbody>().isKinematic = false;
                bodyPartToThrow = null;
                rbSpine.AddForce((transform.up - 1.2f * transform.forward) * 100f, ForceMode.Impulse);
            }

            toActivate.GetComponent<Dot>().ActivateDot(span);
        }
    }

    public void grab()
    {

        //float minDistance = 100f;
        //foreach (var bodyPart in bodyParts)
        //{
        //    var distance1 = Vector3.Distance(bodyPart.transform.position, rightHand.transform.position);
        //    if (distance1 < minDistance && RagdollMapping[bodyPart] != ragdollPart)
        //    {
        //        minDistance = distance1;
        //        ragdollPart = RagdollMapping[bodyPart];

        //    } 

        //}

        ragdollPart = Hips;
        foreach (var bodyPart in bodyParts)
        {
            if (RagdollMapping[bodyPart] != ragdollPart)
                RagdollMapping[bodyPart].GetComponent<Rigidbody>().isKinematic = false;
        }
        rbLeftForearm.isKinematic = false;
        rbRightForearm.isKinematic = false;
        rbSpine.isKinematic = false;

        transform.gameObject.GetComponent<Animator>().enabled = false;

        gameObject.GetComponent<Collider>().isTrigger=true;

        foreach (var legPart in Legs)
            legPart.GetComponent<Collider>().enabled = true;

    }

    public void release()
    {
        ragdollPart.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

}

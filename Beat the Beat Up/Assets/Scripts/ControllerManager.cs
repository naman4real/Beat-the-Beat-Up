using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    public OVRInput.Controller controller;

    public GameObject Enemy;
    private Vector3 EnemyPos;
    private Enemy EnemyScript;
    private Rigidbody EnemyRigidBody;
    private bool GrabActive = false;
    public int GrabDistance = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Enemy = GameObject.FindWithTag("Enemy");
        EnemyPos = Enemy.transform.position;
        EnemyScript = Enemy.GetComponent<Enemy>();
        EnemyRigidBody = Enemy.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        Vector3 pos = OVRInput.GetLocalControllerPosition(controller);
        // Convert this position to game world space
        Vector3 GamePos = transform.TransformPoint(pos);
        Quaternion rot = OVRInput.GetLocalControllerRotation(controller);
        Vector3 ControllerAim = rot * GamePos;
        ControllerAim = ControllerAim.normalized;
        
        if (OVRInput.Get(OVRInput.Button.One))
        {
            
            Vector3 HitRay = transform.position + GrabDistance * ControllerAim;
            Vector3 EnemyRay = EnemyPos - transform.position;
            float EnemyDist = EnemyRay.magnitude;
            float EnemyCrossProduct = Vector3.Cross(HitRay, EnemyRay).magnitude / EnemyDist;
            Debug.Log(EnemyCrossProduct);
            if (EnemyCrossProduct < 2.0f)
            {
                // Enemy in range!
                Enemy.transform.gameObject.GetComponent<Animator>().enabled = false;
                
            }
        }
        

        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) 
        {
            Enemy.transform.gameObject.GetComponent<Animator>().enabled = true;
        }
        // S
    }
}

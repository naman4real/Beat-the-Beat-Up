using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrepuscularRay : MonoBehaviour
{
    private OVRInput.Controller LTouch_Controller = OVRInput.Controller.LTouch;
    private OVRInput.Controller RTouch_Controller = OVRInput.Controller.RTouch;
    // Start is called before the first frame update

    public float ControllerGap = 1.0f;
    public float ControllerDegreeGap = 50.0f;

    public float BlastFovDegrees = 60.0f;

    public float HoldTriggerActivationTime = 1.0f;

    private float BlastIncrements = 2.0f;
    public GameObject LaserBeam;
    private GameObject SpawnLaser;
    private Vector3 SpawnLaserForward;
    bool BeamFired = false;
    float BeamTimer = 0;
    float HoldTriggerTimer = 0.0f;
    // MeshCollider meshCollider;

    // public GameObject Projectile;
    // Vector3 ProjectileDirection;
    void Start()
    {
        SpawnLaser = Instantiate(LaserBeam);
        SpawnLaser.SetActive(false);
        SpawnLaserForward = SpawnLaser.transform.forward;


        // LineRenderer lineRenderer = SpawnLaser.GetComponentInChildren<LineRenderer>();
        // meshCollider = SpawnLaser.transform.GetChild(0).gameObject.AddComponent<MeshCollider>();
        
        // Mesh mesh = new Mesh();
        // lineRenderer.BakeMesh(mesh, true);
        // meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if (!BeamFired && OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            HoldTriggerTimer += Time.deltaTime;
            float ActivationPotential = HoldTriggerTimer / HoldTriggerActivationTime;
            if (ActivationPotential > 1.0f) ActivationPotential = 1.0f;
            OVRInput.SetControllerVibration(ActivationPotential, ActivationPotential, LTouch_Controller);
            OVRInput.SetControllerVibration(ActivationPotential, ActivationPotential, RTouch_Controller);
        }
        else
        {
            HoldTriggerTimer = 0.0f;
        }


        Vector3 LPos = OVRInput.GetLocalControllerPosition(LTouch_Controller);
        Vector3 RPos = OVRInput.GetLocalControllerPosition(RTouch_Controller);
        
        Quaternion lRot = OVRInput.GetLocalControllerRotation(LTouch_Controller);
        Quaternion rRot = OVRInput.GetLocalControllerRotation(RTouch_Controller);
        
        Vector3 GamePos = transform.TransformPoint(LPos);

        bool isControllersClose = Vector3.Distance(LPos, RPos) < ControllerGap;
        bool isControllersAligned = Quaternion.Angle(lRot, rRot) < ControllerDegreeGap;

        //Debug.Log(Quaternion.Angle(lRot, rRot));

        if (isControllersClose && isControllersAligned && HoldTriggerTimer > HoldTriggerActivationTime && !BeamFired)
        {
            Debug.Log("Fired");

            Vector3 LaserForward = lRot * SpawnLaserForward;
            LaserForward.Normalize();
            LaserForward = Vector3.Reflect(LaserForward, Vector3.up);
            Quaternion LaserRot = Quaternion.LookRotation(LaserForward, SpawnLaser.transform.up);

            GamePos.y = transform.position.y;
            SpawnLaser.transform.position = GamePos;
            SpawnLaser.transform.rotation = LaserRot;
            SpawnLaser.SetActive(true);

            int RaycastCount = (int)(BlastFovDegrees / BlastIncrements);
            LaserForward.y = 0;
            LaserForward.Normalize();
            Vector3 StartForward = LaserForward;
            // Start the laserforward to the left of where it's pointing for blast radius
            float StartAngle = -(BlastFovDegrees/2);
            Quaternion BlastRotator = Quaternion.AngleAxis(BlastIncrements, Vector3.up);

            for (int i = 0; i < RaycastCount; i++)
            {
                StartForward = Quaternion.AngleAxis(StartAngle + i * BlastIncrements, Vector3.up) * LaserForward;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(GamePos, StartForward, out hit, Mathf.Infinity))
                {
                    Debug.Log(hit.transform.root.gameObject.name);
                    {
                        Debug.Log(hit.transform.root.gameObject.name);
                        hit.transform.root.gameObject.GetComponent<Animator>().enabled = false;
                        hit.transform.root.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                        Vector3 PushbackDirection = (-hit.transform.root.forward.normalized + StartForward.normalized) / 2;
                        hit.transform.root.gameObject.GetComponent<Rigidbody>().AddForce((hit.transform.root.up + 1.2f * PushbackDirection.normalized) * 100f, ForceMode.Impulse);
                    }
                }

            }

            OVRInput.SetControllerVibration(0, 0, LTouch_Controller);
            OVRInput.SetControllerVibration(0, 0, RTouch_Controller);

        }

        if (BeamFired)
        {
            
            if (BeamTimer < 5.0f)
            {
                   BeamTimer += Time.deltaTime;
            
            }
            else
            {
                BeamFired = false;
                BeamTimer = 0;
            }
         
        }

        
    }
}

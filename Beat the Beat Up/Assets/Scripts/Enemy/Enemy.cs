using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float runSpeed;
    public float stopDist;
    public float minSpawnDist;
    public float maxSpawnDist;

    private GameObject destination;
    private bool reachDest;

    private GameObject player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // initialize variable
        reachDest = false;

        // get player
        anim = GetComponent<Animator>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
            player = players[0];

        // set enemy destination
        foreach (var point in GameObject.FindGameObjectsWithTag("StopPoint"))
        {
            StopPoint p = point.GetComponent<StopPoint>();
            if (!p.IsTaken())
            {
                Debug.Log(name + " found dest " + p.name);
                p.Take(gameObject);
                SetDestination(point);
                anim.SetBool("run", !reachDest);
                break;
            }
        }
        Vector3 destPos = destination.transform.position;


        // place enemy at some distance away from destination while lining up with the player and enemy destination
        float dist = Random.Range(minSpawnDist, maxSpawnDist);
        Vector3 awayFromPlayer = Vector3.Normalize(destPos - player.transform.position.XZPlane());
        transform.position = destPos + awayFromPlayer * dist;

        // adjust forward direction to face player
        transform.forward = Vector3.Normalize(-1 * awayFromPlayer);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.green, 10);
    }

    private void FixedUpdate()
    {
        if (!reachDest)
        {
            // run
            transform.forward = Vector3.Normalize(player.transform.position.XZPlane() - transform.position.XZPlane());
            transform.position += transform.forward * runSpeed * Time.fixedDeltaTime;

            if (Vector3.Distance(transform.position.XZPlane(), destination.transform.position.XZPlane()) <= stopDist)
            {
                Debug.Log("REACH DESTINATION");
                reachDest = true;
                anim.SetBool("run", !reachDest);
            }
        }
        else
        {
            // face player (with some offset to look better)
            transform.LookAt(player.transform.position + transform.right * 1.5f);
        }
    }

    public void SetDestination(GameObject dest)
    {
        destination = dest;
    }

    public void Kill()
    {
        destination.GetComponent<StopPoint>().Untake();
        Destroy(gameObject);
    }
}

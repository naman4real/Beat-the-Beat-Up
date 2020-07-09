using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runSpeed;
    public float stopDist;

    private GameObject destination;
    private bool reachDest;

    private GameObject player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
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

        // adjust forward direction to face destination
        Vector3 dir = destination.transform.position - transform.position;
        transform.forward = Vector3.Normalize(dir);
        Debug.DrawLine(transform.position, transform.position + dir * 10, Color.green, 20);
    }

    private void FixedUpdate()
    {
        if (!reachDest)
        {
            // face player
            Vector3 dir = destination.transform.position - transform.position;
            transform.forward = Vector3.Normalize(dir);
            // run
            transform.position += transform.forward * runSpeed * Time.fixedDeltaTime;


            if (Vector3.Distance(transform.position, destination.transform.position) <= stopDist)
            {
                Debug.Log("REACH DESTINATION");
                reachDest = true;
                anim.SetBool("run", !reachDest);
            }
        }
        else
        {
            // face player
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

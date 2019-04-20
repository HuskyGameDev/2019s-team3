using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script controls throwing/instantiating new rocks
 */

public class MonkeyThrowingAI : MonoBehaviour
{

    const float throwSpeed = 20;
    GameObject player;
    public Transform guide;
    public GameObject thisMonkey;
    bool isClose;
    const float newRockDelay = 1;
    const float throwDelay = .5f;
    float time;
    public Throwable projectile;
    bool holding = false;
    Throwable currentRock;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        time = Time.realtimeSinceStartup;
        Debug.Log("monkey has tag " + tag);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.realtimeSinceStartup - time) > newRockDelay && !holding)
        {
            GrabRock();
        }
        else if ((Vector3.Distance(guide.position, player.transform.position) < 25) && holding && (Time.realtimeSinceStartup - time) > throwDelay)
        {
            Debug.Log("guide forward: " + guide.forward);
            ShootPlayer();
        }
    }

    protected virtual void GrabRock()
    {
        holding = true;
        currentRock = Instantiate(projectile, guide, true);
        projectile.PickUp(guide, false);
        Debug.Log(guide.position);
        time = Time.realtimeSinceStartup;
    }

    void ShootPlayer()
    {
        guide.DetachChildren();
        Vector3 throwAngle = player.transform.position - guide.transform.position;
        throwAngle.y += 2;
        currentRock.Throw(throwSpeed, throwAngle);
        holding = false;
        time = Time.realtimeSinceStartup;
    }
}
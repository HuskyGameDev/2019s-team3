﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyAI : MonoBehaviour {

    Transform player;
    NavMeshAgent nav;
    Transform thisAI;
    bool isClose;
    public Throwable projectile;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        thisAI = GetComponent<Transform>();
        projectile.PickUp(thisAI);
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(thisAI.position, player.position) < 10)
        {
         
            shootPlayer();
        }
        else
        {
            nav.enabled = true;
            nav.SetDestination(player.position);
            
        }
	}

    void shootPlayer()
    {
        NavMeshHit hit;
        if(nav.Raycast(player.position, out hit))
        {
            Debug.Log("player is targetable");
            
        }

    }
}

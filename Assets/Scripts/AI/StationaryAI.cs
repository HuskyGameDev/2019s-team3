using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script controls targetting behavior
 */

public class StationaryAI : Actor {

    Transform player;
    Transform thisAI;
    bool isClose;

    public override void Damage()
    {
        Die();
    }

    public override void Die()
    {
        FindObjectOfType<GameManager>().PickUpOneCoin();
        Destroy(gameObject, .1f);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisAI = GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player);
	}
}

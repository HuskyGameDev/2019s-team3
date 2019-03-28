using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script controls targetting behavior
 */

public class StationaryAI : MonoBehaviour {

    Transform player;
    Transform thisAI;
    bool isClose;

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

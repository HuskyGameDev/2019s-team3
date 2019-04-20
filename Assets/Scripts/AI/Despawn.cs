using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    public Throwable throwable;
    public float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if (throwable.inHand)
        {
            startTime = Time.realtimeSinceStartup;
            return;
        }

        if (Time.realtimeSinceStartup - startTime > 10)
        {
            Destroy(gameObject);
        }
	}
}

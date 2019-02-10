using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour { 
    public void PickUp()
    {
        
    }

    public void Throw(float throwspeed, Transform guide)
    {
        // set our Gravity to true again.
        this.GetComponent<Rigidbody>().useGravity = true;

        // apply velocity on throwing
        this.GetComponent<Rigidbody>().velocity = new Vector3(guide.forward.x * throwspeed, guide.forward.y + throwspeed / 2 + 2, guide.forward.z * throwspeed);
        Debug.Log(this.GetComponent<Rigidbody>().velocity);

        // re-enable collisions
        this.GetComponent<SphereCollider>().enabled = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

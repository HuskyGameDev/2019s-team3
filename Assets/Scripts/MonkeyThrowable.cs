using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyThrowable : Throwable {

    bool thrown = false;

    public new void Throw(float throwspeed, Transform guide)
    {
        if (!thrown)
        {
            // set our Gravity to true again.
            this.GetComponent<Rigidbody>().useGravity = true;

            // apply velocity on throwing
            this.GetComponent<Rigidbody>().AddForce(guide.forward.x + throwspeed, guide.forward.x + throwspeed, guide.forward.x + throwspeed, 
                ForceMode.Impulse);
            Debug.Log("Velocity after throwing: " + this.GetComponent<Rigidbody>().velocity);


            // re-enable collisions
            this.GetComponent<Collider>().enabled = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

 using UnityEngine;
 using System.Collections;
 
 public class HoldItems : MonoBehaviour {
 
	public float throwSpeed;
	private float originalThrowSpeed;
	public bool canHold = true;
	public GameObject ball;
	public Transform guide;
	private bool charging = false;
	private bool primed = false;
	public float chargeRate = 1;
	
	void Start() {
		originalThrowSpeed = throwSpeed;
	}
 
	void Update()
	{
		float distance;
		
		// If we're currently holding an object
		if (!canHold) {
			if (Input.GetButtonDown("Grab")) {
				charging = true;
			}
			// Increment throw speed
			if (charging == true) {
				if (throwSpeed < originalThrowSpeed * 4) {
					throwSpeed = (throwSpeed + (chargeRate * Time.deltaTime));
				}
			}
			// We let go, stop charging
			if (Input.GetButtonUp("Grab") && charging) {
				charging = false;
				primed = true;
			}
		}
				
		// Throw the ball
		if (!canHold && !charging && primed) {
			Drop();
			Debug.Log("Throwing " + ball);
		// Pick up a ball
		} else if (Input.GetButtonDown("Grab")) {
			distance = Vector3.Distance(ball.transform.position, guide.transform.position);
			Debug.Log("Attempting to pick up " + ball + ", distance = " + distance);
			if (distance < 1.5f) {
				Pickup();
			}
		}
		if (!canHold && ball)
			ball.transform.position = guide.position;       
   }
 
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "ball")
			if (!ball) // if we don't have anything holding
				ball = col.gameObject;
	}
 
	void OnTriggerExit(Collider col)
	{
        if (col.gameObject.tag == "ball") {
			if (canHold)
				ball = null;
		}
	}
 
	private void Pickup()
	{
		if (!ball)
			return;
 
         // set gravity to false while holding it
         ball.GetComponent<Rigidbody>().useGravity = false;
		 
         // re-position the ball on our guide object 
         ball.transform.position = guide.position;
		 
		 // Disable collisions
		 ball.GetComponent<SphereCollider>().enabled = false;
 
         canHold = false;
     }
 
    private void Drop()
	 {
		 if (!ball)
			 return;
 
         // set our Gravity to true again.
         ball.GetComponent<Rigidbody>().useGravity = true;
		  
         // apply velocity on throwing
         ball.GetComponent<Rigidbody>().velocity = new Vector3(guide.forward.x * throwSpeed, guide.forward.y + throwSpeed/2 + 2, guide.forward.z * throwSpeed);
		 Debug.Log(ball.GetComponent<Rigidbody>().velocity);
		 
		 // re-enable collisions
		 ball.GetComponent<SphereCollider>().enabled = true;
         
		 canHold = true;
		 primed = false;
		 throwSpeed = originalThrowSpeed;
     }
 }//class
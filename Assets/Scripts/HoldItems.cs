 using UnityEngine;
 using System.Collections;
 
 public class HoldItems : MonoBehaviour {
 
	public float throwSpeed;
	private float originalThrowSpeed;
	public bool canHold = true;
	public GameObject holditem;
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
			Debug.Log("Throwing " + holditem);
		// Pick up a ball
		} else if (Input.GetButtonDown("Grab")) {
            //TODO: change this to hitscanning - needs to check distance and check object type
			distance = Vector3.Distance(holditem.transform.position, guide.transform.position);
			Debug.Log("Attempting to pick up " + holditem + ", distance = " + distance);
			if (distance < 1.5f) {
				Pickup();
			}
		}
		if (!canHold && holditem)
			holditem.transform.position = guide.position;       
   }
 
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "ball")
			if (!holditem) // if we don't have anything holding
				holditem = col.gameObject;
	}
 
	void OnTriggerExit(Collider col)
	{
        if (col.gameObject.tag == "ball") {
			if (canHold)
				holditem = null;
		}
	}
 
	private void Pickup()
	{
		if (!holditem)
			return;
 
         // set gravity to false while holding it
         holditem.GetComponent<Rigidbody>().useGravity = false;
		 
         // re-position the ball on our guide object 
         holditem.transform.position = guide.position;
		 
		 // Disable collisions
		 holditem.GetComponent<SphereCollider>().enabled = false;
 
         canHold = false;
     }
 
    private void Drop()
	 {
		 if (!holditem)
			 return;

		 canHold = true;
		 primed = false;
		 throwSpeed = originalThrowSpeed;
     }
 }//class
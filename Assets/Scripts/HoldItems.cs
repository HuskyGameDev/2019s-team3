﻿ using UnityEngine;
 using System.Collections;
 
 public class HoldItems : MonoBehaviour {
 
	public float throwSpeed;
	private float originalThrowSpeed;
	public bool canHold = true;
	private GameObject holditem;
	public Transform guide;
    private bool pickingUp = false;
    private const float pickingUpBuffer = 0.3f;
    private float time;
	private bool charging = false;
	private bool primed = false;
	public float chargeRate = 1;
	public RectTransform chargeBar;
	public GameObject canvas;
	
	void Start() {
		originalThrowSpeed = throwSpeed;
		canvas.SetActive(true);
		
	}
 
	void Update()
	{
		Debug.Log(throwSpeed);
		chargeBar.localScale = new Vector3(throwSpeed - originalThrowSpeed, 0.09872f, 1f);
		
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
            if (canHold)
            {
                Debug.Log("Enabling item pick up");
                pickingUp = true;
                time = Time.realtimeSinceStartup;
            }
		}
        if(pickingUp && !Input.GetButtonDown("Grab"))
        {
            if (pickingUpBuffer < Time.realtimeSinceStartup - time)
            {
                Debug.Log("Disabling item pickup");
                pickingUp = false;
            }
        }
		if (!canHold && holditem)
			holditem.transform.position = guide.position;       
   }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object is throwable: " + other.gameObject.GetComponent<Throwable>());
    }

    void OnTriggerStay(Collider col)
	{

        if (pickingUp && col.gameObject.GetComponent<Throwable>() != null)
            if (canHold)
            {// if we don't have anything holding
                float distance;
                holditem = col.gameObject;
                distance = Vector3.Distance(holditem.transform.position, guide.transform.position);
                Debug.Log("Attempting to pick up " + holditem + ", distance = " + distance);
                Pickup();
                pickingUp = false;
            }
	}
 /*
	void OnTriggerExit(Collider col)
	{
        if (col.gameObject.GetComponent<Throwable>()) {
			if (!canHold)
				holditem = null;
		}
	}*/
 
	private void Pickup()
	{
		if (!holditem)
			return;
        holditem.GetComponent<Throwable>().PickUp(guide);
         canHold = false;
     }
 
    private void Drop()
	 {
		 if (!holditem)
			 return;
         holditem.GetComponent<Throwable>().Throw(throwSpeed, guide);
         canHold = true;
		 primed = false;
		 throwSpeed = originalThrowSpeed;
         holditem = null;
     }
 }//class
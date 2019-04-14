using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Throwable {

    private ShootingGalleryManager master;
    private int slaveID;
    private bool active = true;

    public override bool PickUp(Transform guide, bool playerThrown)
    {
        Debug.Log("state: " + active);
        if(!active)
        { 
            base.PickUp(guide, playerThrown);
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Throwable>() && active)
        {
            Debug.Log("deactivating target");
            active = false;
            master.TargetHit();
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log("initializing target");
        master = GameObject.Find("ShootingGalleryManager").GetComponent<ShootingGalleryManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

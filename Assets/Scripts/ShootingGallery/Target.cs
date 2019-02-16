using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Throwable {

    private ShootingGalleryManager master;
    private int slaveID;
    private bool active = true;

    public new void PickUp(Transform guide)
    {
        if(!active)
        {
            (this as Throwable).PickUp(guide);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Throwable>() && active)
        {
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

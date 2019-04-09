using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryThrowable : Throwable {
    //TODO: this script may no longer be needed
    protected bool active = false;
    private bool beingThrown = false;
    private const float delay = .3f;
    private float time;

    public new void Throw(float throwspeed, Transform guide)
    {
        beingThrown = true;
        time = Time.realtimeSinceStartup;
        (this as Throwable).Throw(throwspeed, guide);
    }

    void OnCollisionEnter(Collision col)
    {
        if(active && col.gameObject.GetComponent<Target>() != null)
        {

        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (beingThrown && delay < Time.realtimeSinceStartup - time)
        {
            active = true;
            beingThrown = false;
        }

	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryDoor : GallerySwitchable {
    bool activated = false;
    public float yDelta = 10;

    // Use this for initialization
    void Start () {
        Toggle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Toggle()
    {
        if(!activated)
        {
            Debug.Log("door activating");
            activated = true;
            transform.Translate(0, -yDelta, 0);
        }
        else
        {
            Debug.Log("door deactivating");
            activated = false;
            transform.Translate(0, yDelta, 0);
        }
    }
}

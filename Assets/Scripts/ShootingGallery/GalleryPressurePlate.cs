using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPressurePlate : MonoBehaviour {

    public GallerySwitchable targetSwitchable;

    private bool isColliding = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;
            Debug.Log("Toggling switch");
            targetSwitchable.Toggle();

            transform.Translate(0, -.1f, 0);
            AkSoundEngine.PostEvent("Button", this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.Translate(0, .1f, 0);
        isColliding = false;
        AkSoundEngine.PostEvent("Button", this.gameObject);
    }
}

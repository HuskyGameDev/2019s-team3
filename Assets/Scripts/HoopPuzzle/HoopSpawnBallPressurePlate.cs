using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawnBallPressurePlate : MonoBehaviour {

    private bool isColliding = false;
    public GameObject ball;
    public GameObject guide;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;

            transform.Translate(0, -.1f, 0);
            AkSoundEngine.PostEvent("Button", this.gameObject);

            ball.transform.position = guide.transform.position;
            ball.transform.SetPositionAndRotation(guide.transform.position, guide.transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.Translate(0, .1f, 0);
        isColliding = false;
        AkSoundEngine.PostEvent("Button", this.gameObject);
    }
}

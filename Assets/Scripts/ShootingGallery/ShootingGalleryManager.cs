using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGalleryManager : MonoBehaviour {

    private int points;
    private const int pointsToWin = 10;

    public void TargetHit()
    {
        points++;
        Debug.Log("Target was hit, adding points");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

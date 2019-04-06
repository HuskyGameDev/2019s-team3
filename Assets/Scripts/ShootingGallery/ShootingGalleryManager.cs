using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGalleryManager : MonoBehaviour {

    public GalleryDoor westDoor;
    public GalleryDoor eastDoor;

    private int points;
    private const int pointsToWin = 3;
    private const int stage2Points = 1;

    public void TargetHit()
    {
        points++;
        UpdateStage();
        Debug.Log("Target was hit, adding points");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateStage()
    {
        if(points == stage2Points)
        {
            westDoor.Toggle();
            eastDoor.Toggle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntItem : MonoBehaviour {

    public bool HasKey = false;
    public float SearchVisibleDistance = 5.0f;
    public CanvasGroup canvasGroup;

    private bool foundKey = false;
    private GameObject camera;
    private GameObject player;
    private GameManager gameManager;
    private string[] foxtailTaunts;

    // Use this for initialization
    void Start() {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();

        foxtailTaunts = new string[] {
            "I already looked under that one.", 
            "Nope.",
            "Nothing here, just like every other rock!",
            "Feel free to keep looking.",
            "Nice try, maybe the next one will have the fragment?",
            "Ouch, nothing under there.",
        };
	}
	
	// Update is called once per frame
	void Update () {
        bool inRange = Vector3.Distance(transform.position, player.transform.position) < SearchVisibleDistance;
        // hide the element if the player is not close to it
        canvasGroup.alpha = inRange ? 1f : 0f;
        if (!inRange) return;

        // Rotate the Canvas so that it faces the camera
        Vector3 targetPostition = new Vector3(camera.transform.position.x,
                                        transform.position.y,
                                        camera.transform.position.z);
        transform.LookAt(targetPostition);

        // Check to see if the player wants to search this item.
        if (Input.GetButtonDown("Search"))
        {
            if (HasKey && !foundKey)
            {
                foundKey = true;
                gameManager.ShowDialog("Drat! That wasn't there when I looked.");
                gameManager.AddOneKeyFragment();
            }
            else
            {
                gameManager.ShowDialog(foxtailTaunts[new System.Random().Next(foxtailTaunts.Length)]);
            }
        }
	}
}

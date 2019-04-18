using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTempleDoor : MonoBehaviour {

    public GameObject[] doorObjects;
    private GameManager gameManager;
    public int KeyFragmentsRequired = 1;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (gameManager.GetFragments() >= KeyFragmentsRequired)
        {
            foreach (var obj in doorObjects)
            {
                Destroy(obj);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.GetFragments() < KeyFragmentsRequired && other.tag == "Player")
        {
            gameManager.ShowDialog("You must find all " + KeyFragmentsRequired + " hidden key fragments to gain access to the temple!");
        }
    }
}

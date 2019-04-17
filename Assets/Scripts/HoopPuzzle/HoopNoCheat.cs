using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopNoCheat : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - gameObject.transform.position;
            direction.Normalize();
            FindObjectOfType<PlayerController>().Knockback(direction);
            gameManager.ShowDialog("No cheating!");
        }
    }
}

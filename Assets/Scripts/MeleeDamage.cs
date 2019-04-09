using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour {

    private List<GameObject> snakes;
    private System.Random rand;
    private GameObject player;

	// Use this for initialization
	void Start () {
        snakes = new List<GameObject>();
        rand = new System.Random();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Grab") && snakes.Count > 0)
        {
            Debug.Log("trying to do something");
            var snakeObject = snakes[rand.Next(snakes.Count)];

            Vector3 hitDirection = player.transform.position - snakeObject.transform.position;
            hitDirection = hitDirection.normalized;
            snakeObject.transform.position += 2 * hitDirection;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something has entered: " + other.tag);
        if (other.gameObject.tag == "Snake")
        {
            snakes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Snake" && snakes.Contains(other.gameObject))
        {
            snakes.Remove(other.gameObject);
        }
    }
}

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
    void Update()
    {
        if (Input.GetButtonDown("Grab") && snakes.Count > 0)
        {
            var snakeObject = snakes[rand.Next(snakes.Count)];

            if (snakeObject.GetComponent<WanderingAI>().health > 1)
            {
                Vector3 hitDirection = snakeObject.transform.position - player.transform.position;
                hitDirection = hitDirection.normalized;
                snakeObject.transform.position += 2 * hitDirection;
            }
            else
            {
                snakes.Remove(snakeObject);
            }

            snakeObject.GetComponent<Actor>().Damage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            snakes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            snakes.Remove(other.gameObject);
        }
    }
}

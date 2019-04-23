using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public bool inHand = false;
    bool live = false; // gameobject is in motion and should damage actors that it collides with
    bool playerThrown;

    public virtual bool PickUp(Transform guide, bool playerThrown)
    {
        inHand = true;

        this.playerThrown = playerThrown;
        live = false;
        // set gravity to false while holding it
        this.GetComponent<Rigidbody>().useGravity = false;

        // re-position the ball on our guide object 
        this.transform.position = guide.position;

        // Disable collisions
        this.GetComponent<Collider>().enabled = false;

        return true;
    }

    void PreThrow()
    {
        // re-enable collisions
        this.GetComponent<Collider>().enabled = true;

        // allow this object to damage actors
        live = true;

        // This object is no longer being held
        inHand = false;

        // set our Gravity to true again.
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    public virtual void Throw(float throwspeed, Vector3 direction)
    {
        PreThrow();
        this.GetComponent<Rigidbody>().AddForce(
               new Vector3(direction.x * throwspeed, 
                        (direction.y >= 0) ? direction.y * throwspeed/10 + 2 : (direction.y * throwspeed) * .8f,
                        direction.z * throwspeed),
               ForceMode.Impulse);
        Debug.Log("Velocity after throwing: " + this.GetComponent<Rigidbody>().velocity);
    }

    public virtual void Throw(float throwspeed, Transform guide)
    {
        PreThrow();

        // apply velocity on throwing
        this.GetComponent<Rigidbody>().velocity = playerThrown ?
            new Vector3(guide.forward.x * throwspeed, (float)Math.Pow(throwspeed / 5, 3) + 2, guide.forward.z * throwspeed) :
            new Vector3(guide.forward.x * throwspeed, 0, guide.forward.z * throwspeed);
        Debug.Log("Velocity after throwing: " + this.GetComponent<Rigidbody>().velocity);
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
	}

    private void OnCollisionEnter(Collision collision)
    {
        Interact(collision.gameObject, collision.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact(other.gameObject, other.transform);
    }

    private void Interact(GameObject gameObject, Transform t)
    {
        if (live)
        {
            //object should check if it hit the ground, 
            //otherwise it should try to damage the player/actor it hit
            if (gameObject.name == "Terrain")
            {
                live = false;
                Debug.Log("Rock has hit terrain");
            }
            else if (gameObject.CompareTag("Player") && !playerThrown)
            {
                Debug.Log("Rock has hit player");
                //throwable has hit the player
                Vector3 hitDirection = t.position - transform.position;
                hitDirection = hitDirection.normalized;
                FindObjectOfType<GameManager>().RemoveHearts(1, hitDirection);
            }
            else if (gameObject.CompareTag("Enemy") && playerThrown)
            {
                Actor actor = gameObject.GetComponent<Actor>();
                Debug.Log("Rock has hit actor " + gameObject.name);
                //throwable has hit an enemy
                actor.Die();
            }

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    bool live = false; // gameobject is in motion and should damage actors that it collides with
    bool playerThrown;

    public virtual bool PickUp(Transform guide, bool playerThrown)
    {
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

    public virtual void Throw(float throwspeed, Transform guide)
    {
        // re-enable collisions
        this.GetComponent<Collider>().enabled = true;

        // allow this object to damage actors
        live = true;

        // set our Gravity to true again.
        this.GetComponent<Rigidbody>().useGravity = true;

        // apply velocity on throwing
        this.GetComponent<Rigidbody>().velocity = new Vector3(guide.forward.x * throwspeed,
            (float)Math.Pow(throwspeed / 5, 3) + 2, guide.forward.z * throwspeed);
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
        if (live)
        {
            //object should check if it hit the ground, 
            //otherwise it should try to damage the player/actor it hit
            if (collision.gameObject.name == "Terrain")
            {
                live = false;
                Debug.Log("Rock has hit terrain");
            }
            else if(collision.gameObject.CompareTag("Player") && !playerThrown)
            {
                Debug.Log("Rock has hit player");
                //throwable has hit the player
                Vector3 hitDirection = collision.transform.position - transform.position;
                hitDirection = hitDirection.normalized;
                FindObjectOfType<GameManager>().RemoveHearts(1, hitDirection);
            }
            else if(collision.gameObject.CompareTag("Enemy") && playerThrown)
            {
                Actor actor = collision.gameObject.GetComponent<Actor>();
                Debug.Log("Rock has hit actor " + collision.gameObject.name);
                //throwable has hit an enemy
                actor.Damage();
            }

        }
    }
}

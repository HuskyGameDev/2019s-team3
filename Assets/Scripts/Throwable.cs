﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    bool live = false; // gameobject is in motion and should damage actors that it collides with
    GameObject thrower;

    public virtual void PickUp(Transform guide, GameObject thrower)
    {
        this.thrower = thrower;
        live = false;
        // set gravity to false while holding it
        this.GetComponent<Rigidbody>().useGravity = false;

        // re-position the ball on our guide object 
        this.transform.position = guide.position;

        // Disable collisions
        this.GetComponent<Collider>().enabled = false;
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
            throwspeed / 10 + 2, guide.forward.z * throwspeed);
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
        if(live)
        {
            //object should check if it hit the ground, 
            //otherwise it should try to damage the player/actor it hit
            Actor actor;
            if(collision.gameObject.name == thrower.name)
            {
                return;
            }
            else if (collision.gameObject.name == "Terrain")
            {
                live = false;
                Debug.Log("throwable hit the ground");
            }
            else if(collision.gameObject.tag == "Player")
            {
                //throwable has hit the player
                Vector3 hitDirection = collision.transform.position - transform.position;
                hitDirection = hitDirection.normalized;
                FindObjectOfType<GameManager>().RemoveHearts(1, hitDirection);
            }
            else if( (actor = collision.gameObject.GetComponent<Actor>()) != null)
            {
                //throwable has hit an enemy
                actor.Damage();
            }

        }
    }
}

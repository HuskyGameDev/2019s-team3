using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemTileCollider : MonoBehaviour {

    private MemoryGame master;
    private int slaveID;
    private bool isColliding = false;

    // Use this for initialization
    void Start () {
        master = GameObject.Find("MemoryGameBounds").GetComponent<MemoryGame>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void setID(int ID)
    {
        slaveID = ID;
    }

    public void setMaterial(Material m)
    {
        GetComponent<MeshRenderer>().material = m;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;

            master.receiveInput(slaveID);
            transform.Translate(0, -.1f, 0);
            AkSoundEngine.PostEvent("Button", this.gameObject);
        }
    }
       
    private void OnTriggerExit(Collider other)
    {
        transform.Translate(0, .1f, 0);
        isColliding = false;
        AkSoundEngine.PostEvent("Button", this.gameObject);
    }
}

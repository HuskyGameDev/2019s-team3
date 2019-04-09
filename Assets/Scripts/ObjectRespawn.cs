using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cube")
        {
            this.transform.position = new Vector3(100.9f, 3.03f, 429.73f);
        }
    }
}

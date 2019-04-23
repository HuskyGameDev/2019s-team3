using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScore : MonoBehaviour
{

    private bool scored = false;
    private HoopManager manager;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<HoopManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Ball" && !scored)
        {
            scored = true;
            manager.Score();
        }
    }


}
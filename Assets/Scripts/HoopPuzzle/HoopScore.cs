using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScore : MonoBehaviour
{

    private bool gotKey = false;
    int ringScore = 0;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ringScore++;
        if (ringScore == 3)
        {
            gotKey = true;
            gameManager.AddOneKeyFragment();
        }
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour {

    private int numScores = 0;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
    }
	
    public void Score()
    {
        numScores++;
        if (numScores == 3)
        {
            gameManager.AddOneKeyFragment();
        }
        else
        {
            AkSoundEngine.PostEvent("Reggie_Success", this.gameObject);
        }
    }
}

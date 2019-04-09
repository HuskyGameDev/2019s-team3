using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MazeAreaTrigger : MonoBehaviour {
	private int dialogStep = -1;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(dialogStep){
		case 0:
			if(!gameManager.IsShowingDialog()){
				gameManager.ShowDialog("Welcome to Maze of Confusion, Reggie!");
				dialogStep++;
	}
			break;
		case 1:
			if(!gameManager.IsShowingDialog()){
				gameManager.ShowDialog("If you can get through this maze, you may find the key to success!");
				dialogStep = -1;
			}
			break;
	}
}

    public void OnTriggerEnter(Collider other)
    {
        dialogStep = 0;
    }

    public void OnTriggerExit(Collider other)
    {
        dialogStep = 5;
    }
}
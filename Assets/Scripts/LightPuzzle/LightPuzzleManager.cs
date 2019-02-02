using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LightPuzzleManager : MonoBehaviour
{
    public GameObject[] squares;

    private LightPuzzlePressurePlate[] plates;
    private bool puzzleSolved = false;
    private int initialSelection = -1;
    private bool initialStateReset = false;
    private GameManager gameManager;
    private int dialogStep = -1;

    // Use this for initialization
    void Start()
    {
        plates = new LightPuzzlePressurePlate[squares.Length];
        for (int i = 0; i < squares.Length; i++)
        {
            plates[i] = squares[i].GetComponent<LightPuzzlePressurePlate>();
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialStateReset)
        {
            initialStateReset = true;

            initialSelection = new System.Random().Next(3);
            ResetBoard(initialSelection);
        }

        var success = true;
        for (int i = 0; i < plates.Length; i++)
        {
            if (plates[i].GetState())
            {
                success = false;
                break;
            }
        }

        switch(dialogStep)
        {
            case 0:
                if (!gameManager.IsShowingDialog())
                {
                    gameManager.ShowDialog("You have discovered the light puzzle! Turn off all the lights to get a key fragment.");
                    dialogStep++;
                }
                break;
            case 1:
                if (!gameManager.IsShowingDialog())
                {
                    gameManager.ShowDialog("If you want to reset the lights, stand on the red pressure plate.");
                    dialogStep++;
                }
                break;
            case 2:
                if (!gameManager.IsShowingDialog())
                {
                    gameManager.ShowDialog("I'm sure you'll have to do that a lot. I doubt you can even get this key fragment.");
                    dialogStep = -1;
                }
                break;
            case 3:
                if (!gameManager.IsShowingDialog())
                {
                    gameManager.ShowDialog("Drat! You may have won this time, but you'll never get the treasure!");
                    dialogStep = -1;
                }
                break;
        }

        if (success && !this.puzzleSolved)
        {
            dialogStep = 3;
            gameManager.AddOneKeyFragment();
        }

        this.puzzleSolved = this.puzzleSolved || success;
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetBoard(initialSelection);
    }

    private void ResetBoard(int board)
    {
        for (int i = 0; i < plates.Length; i++)
        {
            plates[i].SetState(false);
        }

        if (board == 0)
        {
            plates[0].SetState(true);
            plates[6].SetState(true);
            plates[12].SetState(true);
            plates[18].SetState(true);
            plates[24].SetState(true);
        }
        else if (board == 1)
        {
            plates[1].SetState(true);
            plates[2].SetState(true);
            plates[3].SetState(true);
            plates[4].SetState(true);
            plates[8].SetState(true);
            plates[13].SetState(true);
        }
        else if (board == 2)
        {
            plates[0].SetState(true);
            plates[2].SetState(true);
            plates[5].SetState(true);
            plates[7].SetState(true);
            plates[10].SetState(true);
            plates[13].SetState(true);
            plates[14].SetState(true);
            plates[22].SetState(true);
            plates[23].SetState(true);
            plates[24].SetState(true);
        }
    }

    public void PlayerEnteredPuzzleArea()
    {
        if (puzzleSolved)
        {
            gameManager.ShowDialog("You have already solved this puzzle! Go find another key fragment somewhere else.");
        }
        else
        {
            dialogStep = 0;
        }
    }
}

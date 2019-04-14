using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int currentCoins = 0;
    public int currentHearts = 3;
    public int current1Ups = 4;
    public int currentKeyFragments = 0;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public Text CoinText;
    public Text OneUpText;
    public Text KeyFragmentText;
    public GameObject DialogBackground;
    public GameObject DialogHead;
    public Text DialogText;

    private PlayerController player;

    private int heart1BlinkCount = 0;
    private int heart2BlinkCount = 0;
    private int heart3BlinkCount = 0;
    private bool heart1Blinking = false;
    private bool heart2Blinking = false;
    private bool heart3Blinking = false;

    public float DialogTextRenderRate = 0.1f;
    public float DialogTextShowDuration = 4.0f;
    public float HeartBlinkRate = 0.1f;
    public int HeartBlinkCount = 15;

    private string remainingDiaglogText = "";
    private bool dialogTextRendering = false;
    private Queue<string> dialogQueue;
    private DateTime lastClearRequestTime;
    
    private bool isImmune = false;

    // Use this for initialization
    void Start()
    {
        if (HeartBlinkCount % 2 == 0)
        {
            HeartBlinkCount++; // must be an odd number or it won't work. 
        }
        DialogBackground.SetActive(false);
        DialogHead.SetActive(false);
        DialogText.gameObject.SetActive(false);

        dialogQueue = new Queue<string>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogTextRendering && dialogQueue.Count > 0)
        {
            DisplayDialog(dialogQueue.Dequeue());
        }
    }

    public void SetImmunity(bool i)
    {
        isImmune = i;
    }

    public bool GetImmunity()
    {
        return isImmune;
    }

    public void SetOneUps(int oneups)
    {
        current1Ups = oneups;
        OneUpText.text = current1Ups.ToString() + " x ";
    }

    public void RemoveOneUp()
    {
        SetOneUps(current1Ups - 1);
        Score.subtractScore(40);
    }

    public void AddOneUp()
    {
        SetOneUps(current1Ups + 1);
        Score.addScore(30);
    }

    public int GetOneUp()
    {
        return current1Ups;
    }

    public void RemoveCoins(int coins)
    {
        SetCoins(currentCoins - coins);
    }

    public void AddCoins(int coins)
    {
        SetCoins(currentCoins + coins);
        Score.addScore(coins);
    }

    public void SetCoins(int coins)
    {
        currentCoins = coins;
        CoinText.text = "x " + coins.ToString().PadLeft(3, '0');
    }

    public void PickUpOneCoin()
    {
        SetCoins(currentCoins + 1);
        Score.addScore(1);
    }

    public int GetCoins()
    {
        return currentCoins;
    }

    public void RemoveHearts(int hearts, Vector3 direction = new Vector3())
    {
        if (!isImmune)
        {
            SetHearts(currentHearts - hearts, direction);
        }
    }

    public void AddHearts(int hearts)
    {
        SetHearts(currentHearts + hearts);
    }

    public void SetHearts(int hearts, Vector3 direction = new Vector3())
    {
        int previousHearts = currentHearts;
        currentHearts = hearts;
        if (previousHearts < currentHearts)
        {
            if (currentHearts >= 1)
            {
                CancelBlink1();
            }
            if (currentHearts >= 2)
            {
                CancelBlink2();
            }
            if (currentHearts >= 3)
            {
                CancelBlink3();
            }
            Heart1.SetActive(hearts >= 1);
            Heart2.SetActive(hearts >= 2);
            Heart3.SetActive(hearts >= 3);
        }
        else if (previousHearts > currentHearts)
        {
            AkSoundEngine.PostEvent("Reggie_Hurt", this.gameObject);
            if (currentHearts == 0) // if the hearts is 0, then the player died so don't knock them back on respawn
            {
                direction = new Vector3();
            }
            if (direction.magnitude != 0) // apply knockback if it is present
            {
                player.Knockback(direction);
            }
            if (currentHearts < 3 && previousHearts >= 3)
            {
                heart3Blinking = true;
                StartBlink(3);
            }
            if (currentHearts < 2 && previousHearts >= 2)
            {
                heart2Blinking = true;
                StartBlink(2);
            }
            if (currentHearts < 1 && previousHearts >= 1)
            {
                // we don't actually want the last heart to blink away, because the player died
                //StartBlink(1);
            }
        }
    }

    public void PickUpOneHeart()
    {
        SetHearts(currentHearts + 1);
        AkSoundEngine.PostEvent("Reggie_Success", this.gameObject);
    }

    public int GetHearts()
    {
        return currentHearts;
    }

    public void SetKeyFragments(int fragments)
    {
        if (fragments > this.currentKeyFragments)
        {
            AkSoundEngine.PostEvent("Reggie_Success", this.gameObject);
        }
        this.currentKeyFragments = fragments;
        KeyFragmentText.text = "x " + fragments;
    }

    public void AddOneKeyFragment()
    {
        SetKeyFragments(currentKeyFragments + 1);
        AkSoundEngine.PostEvent("Checkpoint", this.gameObject);
        Score.addScore(50);
    }

    public int GetFragments()
    {
        return this.currentKeyFragments;
    }

    private void CancelBlink1()
    {
        CancelInvoke("ToggleHeart1");
        heart1Blinking = false;
        heart1BlinkCount = 0;
        Heart1.SetActive(true);
    }

    private void CancelBlink2()
    {
        CancelInvoke("ToggleHeart2");
        heart2Blinking = false;
        heart2BlinkCount = 0;
        Heart2.SetActive(true);
    }

    private void CancelBlink3()
    {
        CancelInvoke("ToggleHeart3");
        heart3Blinking = false;
        heart3BlinkCount = 0;
        Heart3.SetActive(true);
    }

    private void StartBlink(int heart)
    {
        if (heart == 1 && heart1BlinkCount > 0 || heart == 2 && heart2BlinkCount > 0 || heart == 3 && heart3BlinkCount > 0)
        {
            return;
        }

        InvokeRepeating("ToggleHeart" + heart, 0.0f, HeartBlinkRate);
    }

    private void ToggleHeart1()
    {
        if (!heart1Blinking) return;
        Heart1.SetActive(!Heart1.activeSelf);
        heart1BlinkCount++;
        if (heart1BlinkCount == HeartBlinkCount)
        {
            heart1BlinkCount = 0;
            CancelInvoke("ToggleHeart1");
        }
    }

    private void ToggleHeart2()
    {
        if (!heart2Blinking) return;
        Heart2.SetActive(!Heart2.activeSelf);
        heart2BlinkCount++;
        if (heart2BlinkCount == HeartBlinkCount)
        {
            heart2BlinkCount = 0;
            CancelInvoke("ToggleHeart2");
        }
    }

    private void ToggleHeart3()
    {
        if (!heart3Blinking) return;
        Heart3.SetActive(!Heart3.activeSelf);
        heart3BlinkCount++;
        if (heart3BlinkCount == HeartBlinkCount)
        {
            heart3BlinkCount = 0;
            CancelInvoke("ToggleHeart3");
        }
    }

    public void ShowDialog(string text)
    {
        if (dialogTextRendering)
        {
            dialogQueue.Enqueue(text);
            return;
        }
        DisplayDialog(text);
    }

    private void DisplayDialog(string text)
    {
        dialogTextRendering = true;
        remainingDiaglogText = text;

        DialogText.text = "";
        DialogBackground.SetActive(true);
        DialogHead.SetActive(true);
        DialogText.gameObject.SetActive(true);

        InvokeRepeating("RenderDialogText", 0.0f, DialogTextRenderRate);
    }

    public bool IsShowingDialog()
    {
        return DialogBackground.activeSelf;
    }

    private void RenderDialogText()
    {
        DialogText.text += remainingDiaglogText.Substring(0, 1);
        remainingDiaglogText = remainingDiaglogText.Substring(1);

        if (remainingDiaglogText.Equals(""))
        {
            dialogTextRendering = false;
            CancelInvoke("RenderDialogText");

            lastClearRequestTime = DateTime.Now;
            Invoke("ClearDialog", DialogTextShowDuration);
        }
    }

    private void ClearDialog()
    {
        // don't clear dialog if there is another dialog text currently rendering,
        // or if it hasn't been long enough. This case may arise if something causes the
        // text show duration for a previous dialog to be interrupted by a new dialog.
        if ((DateTime.Now - lastClearRequestTime).TotalSeconds > DialogTextShowDuration - .1
            && !dialogTextRendering)
        {
            DialogBackground.SetActive(false);
            DialogHead.SetActive(false);
            DialogText.gameObject.SetActive(false);
        }
    }
}

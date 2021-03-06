﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayValue : MonoBehaviour {

    public Text textComponent;

    void Awake () {
        //If text hasn't been assigned, disable ourselves
        if (textComponent == null)
        {
            Debug.Log("You must assign a text component!");
            this.enabled = false;
            return;
        }
        UpdateText(Score.getScore());
    }

    void UpdateText (int value) {
        //Update the text shown in the text component by setting the `text` variable
        textComponent.text = "" + value;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameViewController : MonoBehaviour {

    public TextMeshProUGUI pointsText;
    public Button pauseButton;
    public Button foundBugButton;
    public TextMeshProUGUI foundBugButtonText;

    // Use this for initialization
    void Start () {
        pauseButton.onClick.AddListener(OnClickPauseButton);
        foundBugButton.onClick.AddListener(OnClickFoundBugButton);
    }

    void OnClickPauseButton()
    {
        PauseMenuViewController.CreateViewController();
    }

    void OnClickFoundBugButton()
    {
        if (GameModeManager.Instance.isInFindBugMode)
        {
            GameModeManager.Instance.GetIntoPlayMode();
        }
        else
        {
            GameModeManager.Instance.GetIntoFindBugMode();
        }
    }

    public void UpdateUI()
    {
        if (GameModeManager.Instance.isInFindBugMode)
        {
            foundBugButtonText.text = "Back To Play";
        }
        else
        {
            foundBugButtonText.text = "FOUND IT!";
        }
    }
	
	// Update is called once per frame
	void Update () {
        pointsText.text = CurrencyManager.Instance.AmountOfCurrency("points").ToString();
	}
}

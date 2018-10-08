using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : Singleton<GameModeManager>
{

    GameViewController gameViewController;

    public void Init()
    {
        gameViewController = FindObjectOfType<GameViewController>();
    }

    public bool isInFindBugMode;

    public void GetIntoFindBugMode()
    {
        isInFindBugMode = true;
        gameViewController.UpdateUI();
    }

    public void GetIntoPlayMode()
    {
        isInFindBugMode = false;
        gameViewController.UpdateUI();
    }
}

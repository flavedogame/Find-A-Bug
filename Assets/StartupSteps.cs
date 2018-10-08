﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSteps : MonoBehaviour {

    private void Awake()
    {
        SQLiteDatabaseManager.Instance.Init();

        CheatSettings.Instance.Init();
        CurrencyManager.Instance.Init();

        AchievementManager.Instance.Init();
        NarrativeManager.Instance.Init();
        NarrationManager.Instance.Init();
        BugableObjectFunctionManager.Instance.Init();
        BugableObjectManager.Instance.Init();
        GameLogicManager.Instance.Init();
        GameModeManager.Instance.Init();
        //CheatManager.Instance.Init();
        //CurrencyManager.Instance.Init();
        //AbilityManager.Instance.Init();
        //TutorialManager.Instance.Init();
        //MonsterManager.Instance.Init();
        //LevelManager.Instance.Init();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

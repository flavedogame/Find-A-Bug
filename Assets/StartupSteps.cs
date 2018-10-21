using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSteps : MonoBehaviour {

    private void Awake()
    {
        SQLiteDatabaseManager.Instance.Init();
        CheatSettings.Instance.InitDatabase();

        CurrencyManager.Instance.Init();


        AchievementManager.Instance.Init();
        NarrativeManager.Instance.Init();
        NarrationManager.Instance.Init();
        BugableObjectFunctionManager.Instance.Init();
        BugableObjectManager.Instance.Init();
        GameLogicManager.Instance.Init();
        GameModeManager.Instance.Init();
        AttackManager.Instance.Init();


        CheatSettings.Instance.InitCsv();


        TodoListManager.Instance.Init();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSettings : Singleton<CheatSettings>
{

    public bool skipTestingNarrations;
    public bool cleanAchievementWhenStart;
    public bool cleanObjectFunctionWhenStart;
    // Start is called before the first frame update
    void Start()
    {
        if (cleanAchievementWhenStart)
        {
            AchievementManager.Instance.CleanAchievements();

        }
        if (cleanObjectFunctionWhenStart)
        {
            DataService ds = SQLiteDatabaseManager.Instance.ds;
            ds.DeleteAllObjectFunction();
            BugableObjectFunctionManager.Instance.ReadDatabase();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

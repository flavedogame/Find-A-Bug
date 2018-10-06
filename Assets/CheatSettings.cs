using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSettings : Singleton<CheatSettings>
{

    public bool skipTestingNarrations;
    public bool cleanAchievementWhenStart;
    // Start is called before the first frame update
    void Start()
    {
        if (cleanAchievementWhenStart)
        {
            AchievementManager.Instance.CleanAchievements();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

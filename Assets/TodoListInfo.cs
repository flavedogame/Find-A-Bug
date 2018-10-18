using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoListInfo
{

    public string identifier;
    public string description;
    public string startAchievement;
    public string finishAchievement;
    public string endAchievement;
    public string parentList;

    public override string ToString()
    {
        return "AchievementInfo: " + identifier;
    }
}

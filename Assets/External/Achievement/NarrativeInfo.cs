﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeInfo{
    public string identifier;
    public string achievement;
    public string narrativeAction;
    public int delayTime;
    public Dictionary<string,string> param;
    public string finishAchievement;
    public string NarrationId
    {
        get
        {
            return param["narrationID"];
        }
    }

}

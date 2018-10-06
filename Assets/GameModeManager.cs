using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : Singleton<GameModeManager>
{

    public void Init()
    {

    }

    public bool isInFindBugMode;

    public void GetIntoFindBugMode()
    {
        isInFindBugMode = true;
    }

    public void GetIntoPlayMode()
    {
        isInFindBugMode = false;
    }
}

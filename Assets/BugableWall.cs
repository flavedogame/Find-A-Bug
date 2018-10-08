using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableWall : BugableObject
{

    public bool Check_wallBlock_BugTriggered()
    {
        if (CheatSettings.Instance.alwaysTriggeringBugs)
        {
            return true;
        }else
        {
            Vector3 playerPosition = GameLogicManager.Instance.playerObject.transform.position;
            return playerPosition.y > 2 || playerPosition.y < -5 || playerPosition.x > 3 || playerPosition.x < -3;
        }
    }

    protected override string Identifier
    {
        get { return "wall"; }
    }
}

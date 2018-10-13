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
            if (GameLogicManager.Instance.PlayerObject != null) { 
            Vector3 playerPosition = GameLogicManager.Instance.PlayerObject.transform.position;
            return playerPosition.y > 2 || playerPosition.y < -5 || playerPosition.x > 3 || playerPosition.x < -3;
            }
            else
            {
                Debug.LogError("GameLogicManager.Instance.PlayerObject is null");
            }
            return false;
        }
    }


    protected override string Identifier
    {
        get { return "wall"; }
    }
}

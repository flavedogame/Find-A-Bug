using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Singleton<GameLogicManager>
{
    public GameObject PlayerObject { get; private set; }

    Vector3 playerOriginPosition;

    public void SetPlayerObject(GameObject player)
    {
        PlayerObject = player;
        playerOriginPosition = player.transform.position;
    }

    public void ResetPlayerPosition()
    {
        Debug.Log("reset position to " + PlayerObject.transform.position);
        PlayerObject.transform.position = playerOriginPosition;
    }

    public void Init()
    {
    }


}

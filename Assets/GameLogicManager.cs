using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Singleton<GameLogicManager>
{
    public GameObject PlayerObject { get { return playerObject; } }
    GameObject playerObject;
    Vector3 playerOriginPosition;

    public void SetPlayerObject(GameObject player)
    {
        playerObject = player;
        playerOriginPosition = player.transform.position;
    }

    public void ResetPlayerPosition()
    {
        Debug.Log("reset position to " + playerObject.transform.position);
        playerObject.transform.position = playerOriginPosition;
    }

    public void Init()
    {
    }


}

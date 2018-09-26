using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Singleton<GameLogicManager>
{

    public int points;

    public void Init()
    {
    }

    public void addPoints(int amount = 1)
    {
        points += amount;
    }
}

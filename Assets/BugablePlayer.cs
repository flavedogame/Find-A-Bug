using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugablePlayer : BugableObject {

    public bool Check_playerAttack_BugTriggered()
    {
        return false;
    }

    protected override void Start()
    {
        base.Start();
        GameLogicManager.Instance.SetPlayerObject(gameObject);
    }

    protected override string Identifier
    {
        get { return "player"; }
    }
}

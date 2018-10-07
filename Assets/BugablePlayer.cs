using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugablePlayer : BugableObject {

    private void Start()
    {
        GameLogicManager.Instance.playerObject = gameObject;
    }

    protected override string Identifier
    {
        get { return "player"; }
    }
}

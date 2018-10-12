using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableMonster : BugableObject
{

    public bool Check_monsterDead_BugTriggered()
    {
        return false;
    }

    protected override string Identifier
    {
        get { return "monster"; }
    }
}

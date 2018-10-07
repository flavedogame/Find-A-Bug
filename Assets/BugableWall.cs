using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableWall : BugableObject
{

    public bool Check_wallBlock_BugTriggered()
    {
        return true;
    }

    protected override string Identifier
    {
        get { return "wall"; }
    }
}

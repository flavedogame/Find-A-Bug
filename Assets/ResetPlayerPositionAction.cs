using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPositionAction : NarrativeAction
{
    public ResetPlayerPositionAction(NarrativeInfo info) : base(info)
    {
    }

    protected override void P_Enable()
    {
        GameLogicManager.Instance.ResetPlayerPosition();

        BugableObjectManager.Instance.CheckBug();
    }
}

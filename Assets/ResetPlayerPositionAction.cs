using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPositionAction : NarrativeAction
{
    public ResetPlayerPositionAction(NarrativeInfo info) : base(info)
    {
        //Debug.Log("create tutorial action with info: " + narrativeInfo.identifier);
    }

    protected override void P_Enable()
    {
    }
}

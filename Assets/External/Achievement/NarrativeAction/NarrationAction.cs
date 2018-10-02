using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationAction : NarrativeAction
{
        public NarrationAction(NarrativeInfo info) : base(info)
        {

        }

        protected override void P_Enable()
        {
            Debug.Log("enable tutorial action with info: " + narrativeInfo);
        }
    }

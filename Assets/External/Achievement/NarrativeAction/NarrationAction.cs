﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationAction : NarrativeAction
{
        public NarrationAction(NarrativeInfo info) : base(info)
        {
            //Debug.Log("create tutorial action with info: " + narrativeInfo.identifier);
        }

        protected override void P_Enable()
        {
            Debug.Log("enable tutorial action with info: " + narrativeInfo.identifier);
        }
    }

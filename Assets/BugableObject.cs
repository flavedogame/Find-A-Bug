﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableObject : MonoBehaviour {

    BugableObjectInfo info;

	// Use this for initialization
	void Start () {
        info = BugableObjectManager.Instance.bugableObjectInfoDict[Identifier];
        if (info == null)
        {
            Debug.LogError(Identifier + " does not exist in bugable object info dict");
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected string Identifier
    {
        get { return "player"; }
    }

    public void DidTap()
    {
        Debug.Log("did tap " + info.description);
    }
}

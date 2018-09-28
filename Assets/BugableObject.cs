﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableObject : MonoBehaviour {

    BugableObjectInfo info;

    public GameObject alertIcon;

	// Use this for initialization
	void Start () {
        info = BugableObjectManager.Instance.bugableObjectInfoDict[Identifier];
        if (info == null)
        {
            Debug.LogError(Identifier + " does not exist in bugable object info dict");
        }
        UpdateAlertView();
	}

    void UpdateAlertView()
    {
        alertIcon.SetActive(!info.IsFullyViewed);
    }
	
	// Update is called once per frame
	void Update () {
	}

    protected virtual string Identifier
    {
        get { Debug.LogError("should implement in child"); return ""; }
    }

    public void DidTap()
    {
        Debug.Log("did tap " + info.description);
        new BugableObjectStateViewController(info);
    }
}

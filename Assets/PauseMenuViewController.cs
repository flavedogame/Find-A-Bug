﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuViewController : DefaultViewController
{
    public Button resumeButton;
    public Button cheatButton;
    public Button settingButton;
    public Button existButton;

    public PauseMenuViewController()
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[1];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        PauseMenuViewController script = go.GetComponent<PauseMenuViewController>();
        script.Init();
    }

    public void Init()
    {

        resumeButton.onClick.AddListener(Back);
    }

    public void OnClickCheatButton()
    {

    }
}

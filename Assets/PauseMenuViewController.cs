using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuViewController : DefaultViewController
{
    public Button resumeButton;
    public Button cheatButton;
    public Button settingButton;
    public Button achievementButton;
    public Button quitButton;

    static public void CreateViewController()
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[1];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        PauseMenuViewController script = go.GetComponent<PauseMenuViewController>();
        script.Init();
    }

    public void Init()
    {

        resumeButton.onClick.AddListener(Back);
        cheatButton.onClick.AddListener(delegate { CheatMenuViewController.CreateViewController(); });
        quitButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanelViewController : DefaultViewController
{
    public GameObject achievementCell;
    public GameObject achievementListPanel;
    // Use this for initialization

    static public void CreateViewController()
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[4];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        AchievementPanelViewController script = go.GetComponent<AchievementPanelViewController>();
        script.Init();
    }

    void Init()
    {
        foreach (Achievement achievement in AchievementManager.Instance.achievementList)
        {

            GameObject go = Instantiate(achievementCell, achievementListPanel.transform);
            AchievementCell script = go.GetComponent<AchievementCell>();
            script.InitCell(achievement);
        }
    }
}

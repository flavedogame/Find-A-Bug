using System.Collections;
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
        if (narrativeInfo.delayTime > 0)
        {
            MonoBehaviour mb = CoroutineMonobehavior.Instance.GetComponent<MonoBehaviour>();
            mb.StartCoroutine(ShowNarrationAfterTime(narrativeInfo.delayTime));
        }
        else
        {
            ShowNarration();
        }
    }

    void Update()
    {
        //Debug.Log("update action" + identifier);
    }

    void ShowNarration()
    {
        Debug.Log("show narration " + identifier);
        Achievement achievement = AchievementManager.Instance.achievementDictionary[narrativeInfo.achievement];
        if (achievement.state == AchievementState.active)
        {
            if (!NarrationManager.Instance.IsShowingNarrationWithIdentifier(identifier))
        {
            NarrationManager.Instance.ShowNarrationWithIdentifier(narrativeInfo);
        }
        }
        else
        {
            Debug.Log("this dialog is passed");
        }
    }

    IEnumerator ShowNarrationAfterTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
            ShowNarration();
        
    }
}

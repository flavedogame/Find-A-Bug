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
        Debug.Log("enable tutorial action with info: " + narrativeInfo.identifier);
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
        Debug.Log("update action" + identifier);
    }

    void ShowNarration()
    {
        Debug.Log("show narration " + identifier);
        if (!NarrationManager.Instance.IsShowingNarrationWithIdentifier(narrativeInfo.identifier))
        {
            NarrationManager.Instance.ShowNarrationWithIdentifier(narrativeInfo);
        }
    }

    IEnumerator ShowNarrationAfterTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ShowNarration();
    }
}

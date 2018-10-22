using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableObject : MonoBehaviour {

    BugableObjectInfo info;

    public GameObject alertIcon;
    public SpriteRenderer hintSpriteRender;

    bool shouldShowHint;

    Color transparentColor;
    Color redColor;

	// Use this for initialization
	protected virtual void Start () {
        info = BugableObjectManager.Instance.bugableObjectInfoDict[Identifier];
        if (info == null)
        {
            Debug.LogError(Identifier + " does not exist in bugable object info dict");
        }
        UpdateAlertView();
        AddObserveUpdateFunction();
        if (hintSpriteRender != null) { 
        redColor = hintSpriteRender.color;
        transparentColor = new Color(redColor.r, redColor.g, redColor.b, 0);
        }
        else
        {
            Debug.LogError("hintSpriteRender is missing");
        }
        BugableObjectManager.Instance.RegisterBugableObject(this);
        HideHint();
    }

    void AddObserveUpdateFunction()
    {
        BugableObjectFunctionManager.Instance.RegisterCompletionDelegate(delegate {
            //Debug.Log("delegate for function");
            //info = BugableObjectManager.Instance.bugableObjectInfoDict[Identifier];
            UpdateAlertView(); });
    }

    void UpdateAlertView()
    {
        //Debug.Log("info is view " + info.IsFullyViewed);
        alertIcon.SetActive(!info.IsFullyViewed);
    }
	
	// Update is called once per frame
	void Update () {
        ShowHint();
    }

    protected virtual string Identifier
    {
        get { Debug.LogError("should implement in child"); return ""; }
    }

    public void DidTap()
    {
        //Debug.Log("did tap " + info.description);
        if (GameModeManager.Instance.isInFindBugMode)
        {
            FindBug();
        }
        else
        {
            new BugableObjectStateViewController(info);
        }
    }
    
    virtual protected void FindBug()
    {
        bool hasFoundBug = false;
        foreach (BugableObjectFunctionInfo notEnabledFunctionInfo in info.NotEnabledBugableFunctions)
        {
            System.Type T = GetType();
            string checkMethod = "Check_" + notEnabledFunctionInfo.identifier + "_BugTriggered";
            System.Reflection.MethodInfo methodInfo =  T.GetMethod(checkMethod);
            if (methodInfo!=null)
            {
                bool ret = bool.Parse(methodInfo.Invoke(this, null).ToString());
                if (ret)
                {
                    Debug.Log("check passed " + checkMethod + " " + methodInfo.Invoke(this, null).ToString());
                    hasFoundBug = true;
                    AchievementManager.Instance.FinishAchievement(notEnabledFunctionInfo.achievementToFinish);
                    //put these three line in manager
                    //todo: change to based on observer
                    BugableObjectFunctionManager.Instance.ReadCSV();
                    BugableObjectFunctionManager.Instance.ReadDatabase();
                    //BugableObjectFunctionManager.Instance.enabledBugableObjectFunctionInfoDict[info.identifier].Add(notEnabledFunctionInfo);
                    // BugableObjectFunctionManager.Instance.notEnabledBugableObjectFunctionInfoDict[info.identifier].Remove(notEnabledFunctionInfo);
                    BugableObjectFunctionManager.Instance.UpdateFunctionDelegate();

                    GameModeManager.Instance.GetIntoPlayMode();
                    SFXManager.Instance.PlaySFX(SFXEnum.findBug);
                    PopupViewController.CreateFoundItExplosionViewController();

                    
                    
                    break;
                } else
                {
                    Debug.Log("check failed " + checkMethod + " " + methodInfo.Invoke(this, null).ToString());
                }
            } else
            {
                Debug.LogError("function not implemented " + checkMethod);
            }
        }
        if (!hasFoundBug)
        {

            NarrationManager.Instance.ShowNarrationWithIdentifier("nothingSpecial");
            SFXManager.Instance.PlaySFX(SFXEnum.failFindBug);
        }
    }

    public bool IsBugTriggered()
    {
        shouldShowHint = false;
        foreach (BugableObjectFunctionInfo notEnabledFunctionInfo in info.NotEnabledBugableFunctions)
        {
            System.Type T = GetType();
            string checkMethod = "Check_" + notEnabledFunctionInfo.identifier + "_BugTriggered";
            System.Reflection.MethodInfo methodInfo = T.GetMethod(checkMethod);
            if (methodInfo != null)
            {
                bool ret = bool.Parse(methodInfo.Invoke(this, null).ToString());
                if (ret)
                {
                    Debug.Log("check passed " + checkMethod + " " + methodInfo.Invoke(this, null).ToString());
                    shouldShowHint = true;
                    return true;
                }
                else
                {
                    Debug.Log("check failed " + checkMethod + " " + methodInfo.Invoke(this, null).ToString());
                }
            }
            else
            {
                //Debug.LogError("function not implemented " + checkMethod);
            }
        }
        return false;
    }

    void ShowHint()
    {
        if (GameModeManager.Instance.isInFindBugMode)
        {
            if (shouldShowHint)
            {

                hintSpriteRender.color = Color.Lerp(transparentColor, redColor, Mathf.PingPong(Time.time, 0.5f));
            }
            else
            {
                HideHint();
            }
        }
        else
        {
            HideHint();
        }

    }

    void HideHint()
    {
        hintSpriteRender.color = transparentColor;
    }

}

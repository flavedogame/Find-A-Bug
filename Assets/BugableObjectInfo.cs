using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableObjectInfo {
    public string identifier;
    public string description;
    public string name;
    public Sprite icon
    {
        get
        {
            BugableObjectIconEnum iconEnum = (BugableObjectIconEnum)System.Enum.Parse(typeof(BugableObjectIconEnum), identifier);
            return ResourceManager.Instance.bugableObjectIcons[(int)iconEnum];
        }

    }

    public List<BugableObjectFunctionInfo> EnabledBugableFunctions
    {
        get
        {
            return BugableObjectFunctionManager.Instance.enabledBugableObjectFunctionInfoDict[identifier];
        }
    }

    public List<BugableObjectFunctionInfo> NotEnabledBugableFunctions
    {
        get
        {
            return BugableObjectFunctionManager.Instance.notEnabledBugableObjectFunctionInfoDict[identifier];
        }
    }

    public bool IsFullyViewed
    {
        get { 
        foreach(BugableObjectFunctionInfo functionInfo in EnabledBugableFunctions)
        {
            if (!functionInfo.isViewed)
            {
                return false;
            }
        }
        return true;
    }
    }

}

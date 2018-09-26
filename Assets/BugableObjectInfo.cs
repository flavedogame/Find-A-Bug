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

    public List<BugableObjectFunctionInfo> BugableFunctions
    {
        get
        {
            return BugableObjectFunctionManager.Instance.bugableObjectFunctionInfoDict[identifier];
        }
    }

}

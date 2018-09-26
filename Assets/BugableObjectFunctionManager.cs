using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class BugableObjectFunctionManager : Singleton<BugableObjectFunctionManager>
{

    public List<BugableObjectFunctionInfo> bugableObjectFunctionInfoList;
    public Dictionary<string, List<BugableObjectFunctionInfo>> bugableObjectFunctionInfoDict;
    //public Dictionary<string, PersistentBall> ballsOwned;


    //public string currentlyUsingBall;

    public void Init()
    {
        ReadCSV();
        //ReadDatabase();
    }
    void ReadCSV()
    {
        bugableObjectFunctionInfoList = CsvUtil.LoadObjects<BugableObjectFunctionInfo>("bugableObjectFunction.csv");
        bugableObjectFunctionInfoDict = new Dictionary<string, List<BugableObjectFunctionInfo>>();
        foreach (BugableObjectFunctionInfo info in bugableObjectFunctionInfoList)
        {
            if (!bugableObjectFunctionInfoDict.ContainsKey(info.objectId))
            {
                bugableObjectFunctionInfoDict[info.objectId] = new List<BugableObjectFunctionInfo>();
            }
            bugableObjectFunctionInfoDict[info.objectId].Add(info);
        }
        Debug.Log("finish load bugableObjectFunction.csv");
    }
}

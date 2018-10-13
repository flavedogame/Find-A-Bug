using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class BugableObjectManager : Singleton<BugableObjectManager>
{

    public List<BugableObjectInfo> bugableObjectInfoList;
    public Dictionary<string,BugableObjectInfo> bugableObjectInfoDict;
    public bool HasBugTriggered { get; private set; }
    List<BugableObject> bugableObjests;


    //public string currentlyUsingBall;

    public void Init()
    {
        ReadCSV();
        bugableObjests = new List<BugableObject>();
        //ReadDatabase();
    }

    //private void Start()
    //{
    //    ReadCSV();
    //    //ReadDatabase();
    //}
    void ReadCSV()
    {
        //Debug.Log("read csv for bugableObjectManager");
        bugableObjectInfoList = CsvUtil.LoadObjects<BugableObjectInfo>("bugableObject.csv");
        bugableObjectInfoDict = new Dictionary<string, BugableObjectInfo>();
        foreach (BugableObjectInfo info in bugableObjectInfoList)
        {
            bugableObjectInfoDict[info.identifier] = info;
        }
        Debug.Log("finish load bugableObject.csv");
    }

    public void TriggerABug()
    {
        HasBugTriggered |= true;
    }

    public void RegisterBugableObject(BugableObject bo)
    {
        bugableObjests.Add(bo);
    }

    public void UnregisterBugableObject(BugableObject bo)
    {
        bugableObjests.Remove(bo);
    }

    public void CheckBug()
    {
        //todo: check in tutorial, or pay to get a hint
        if (CheatSettings.Instance.alwaysGiveHint)
        {

            HasBugTriggered = false;
            //Debug.Log("check bug " + bugableObjests);
            foreach (BugableObject bo in bugableObjests)
            {
                //Debug.Log("check bug " + bo);
                if (bo.IsBugTriggered())
                {
                    TriggerABug();
                }
            }
        }
    }

}

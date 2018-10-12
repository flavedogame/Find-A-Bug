using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class BugableObjectManager : Singleton<BugableObjectManager>
{

    public List<BugableObjectInfo> bugableObjectInfoList;
    public Dictionary<string,BugableObjectInfo> bugableObjectInfoDict;
    List<BugableObject> bugableObjestsTriggered;


    //public string currentlyUsingBall;

    public void Init()
    {
        ReadCSV();
        bugableObjestsTriggered = new List<BugableObject>();
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

    public void TriggerABug(BugableObject bugableObject)
    {
        bugableObjestsTriggered.Add(bugableObject);
    }

    public void UntriggerABug(BugableObject bugableObject)
    {
        bugableObjestsTriggered.Remove(bugableObject);
    }

}

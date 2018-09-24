using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class BugableObjectManager : Singleton<BugableObjectManager>
{

    public List<BugableObjectInfo> bugableObjectInfoList;
    public Dictionary<string,BugableObjectInfo> bugableObjectInfoDict;
    //public Dictionary<string, PersistentBall> ballsOwned;


    //public string currentlyUsingBall;

    public void Init()
    {
        ReadCSV();
        //ReadDatabase();
    }

    //private void Start()
    //{
    //    ReadCSV();
    //    //ReadDatabase();
    //}
    void ReadCSV()
    {
        bugableObjectInfoList = CsvUtil.LoadObjects<BugableObjectInfo>("bugableObject.csv");
        bugableObjectInfoDict = new Dictionary<string, BugableObjectInfo>();
        foreach (BugableObjectInfo info in bugableObjectInfoList)
        {
            bugableObjectInfoDict[info.identifier] = info;
        }
        Debug.Log("finish load bugableObject.csv");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public delegate void UpdateFunctionDelegate();
public class BugableObjectFunctionManager : Singleton<BugableObjectFunctionManager>
{

    public List<BugableObjectFunctionInfo> bugableObjectFunctionInfoList;
    public Dictionary<string, List<BugableObjectFunctionInfo>> bugableObjectFunctionInfoDict;
    public DataService ds;

    List<UpdateFunctionDelegate> updateFunctionDelegates;


    public void Init()
    {

        ds = SQLiteDatabaseManager.Instance.ds;
        updateFunctionDelegates = new List<UpdateFunctionDelegate>();
        ReadCSV();
        ReadDatabase();
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

            //ReadDatabase(info);

            bugableObjectFunctionInfoDict[info.objectId].Add(info);
        }
        //Debug.Log("finish load bugableObjectFunction.csv");
    }

    public void ReadDatabase()
    {
        foreach (BugableObjectFunctionInfo info in bugableObjectFunctionInfoList)
        {
            ReadDatabase(info);
        }
            foreach (UpdateFunctionDelegate dele in updateFunctionDelegates)
            {
            Debug.Log("dele");
                dele();
            }
        
    }

    void ReadDatabase(BugableObjectFunctionInfo info)
    {
        Debug.Log("read db " + info);
        PersistentObjectFunction persistentObjectFunction = ds.GetPersistentObjectFunction(info.identifier);
        if (persistentObjectFunction == null)
        {
            Debug.Log("persistentObjectFunction == null " + info);
            persistentObjectFunction = new PersistentObjectFunction();
            persistentObjectFunction.identifier = info.identifier;
            info.isViewed = false;
            ds.InsertObjectFunction(persistentObjectFunction);
        }
        else
        {
            Debug.Log("persistentObjectFunction != null " + info);
            info.isViewed = persistentObjectFunction.isViewed;
            
        }
    }
    
    public void ViewFunction(BugableObjectFunctionInfo info)
    {
        PersistentObjectFunction persistentObjectFunction = ds.GetPersistentObjectFunction(info.identifier);
        persistentObjectFunction.isViewed = true;

        ds.UpdateObjectFunction(persistentObjectFunction);
        info.isViewed = true;
        //Debug.Log("updateFunctionDelegates " + updateFunctionDelegates);
        foreach(UpdateFunctionDelegate dele in updateFunctionDelegates)
        {
            dele();
        }
    }

    public  void RegisterCompletionDelegate(UpdateFunctionDelegate dele)
    {
        updateFunctionDelegates.Add(dele);
    }
}

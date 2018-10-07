using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public delegate void UpdateFunctionDelegate();
public class BugableObjectFunctionManager : Singleton<BugableObjectFunctionManager>
{

    public List<BugableObjectFunctionInfo> bugableObjectFunctionInfoList;
    public Dictionary<string, List<BugableObjectFunctionInfo>> enabledBugableObjectFunctionInfoDict;
    public Dictionary<string, List<BugableObjectFunctionInfo>> notEnabledBugableObjectFunctionInfoDict;
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
        enabledBugableObjectFunctionInfoDict = new Dictionary<string, List<BugableObjectFunctionInfo>>();
        notEnabledBugableObjectFunctionInfoDict = new Dictionary<string, List<BugableObjectFunctionInfo>>();
        foreach (BugableObjectFunctionInfo info in bugableObjectFunctionInfoList)
        {
            if (!enabledBugableObjectFunctionInfoDict.ContainsKey(info.objectId))
            {
                enabledBugableObjectFunctionInfoDict[info.objectId] = new List<BugableObjectFunctionInfo>();
                notEnabledBugableObjectFunctionInfoDict[info.objectId] = new List<BugableObjectFunctionInfo>();
            }

            //ReadDatabase(info);
            string prerequisiteString = info.prerequisite;
            if (prerequisiteString.Length != 0)
            {
                Debug.Log("prerequisite for function " + info.identifier + " is " + prerequisiteString);
                Achievement prerequisite = AchievementManager.Instance.achievementDictionary[prerequisiteString];
                if (prerequisite.state != AchievementState.complete)
                {
                    notEnabledBugableObjectFunctionInfoDict[info.objectId].Add(info);
                    continue;
                }
            }
            enabledBugableObjectFunctionInfoDict[info.objectId].Add(info);
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

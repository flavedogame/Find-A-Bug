using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class BugableObjectFunctionManager : Singleton<BugableObjectFunctionManager>
{

    public List<BugableObjectFunctionInfo> bugableObjectFunctionInfoList;
    public Dictionary<string, List<BugableObjectFunctionInfo>> bugableObjectFunctionInfoDict;
    public DataService ds;

    public void Init()
    {

        ds = new DataService("db.s3db");
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

            ReadDatabase(info);

            bugableObjectFunctionInfoDict[info.objectId].Add(info);
        }
        Debug.Log("finish load bugableObjectFunction.csv");
    }

    void ReadDatabase(BugableObjectFunctionInfo info)
    {
        PersistentObjectFunction persistentObjectFunction = ds.GetPersistentObjectFunction(info.identifier);
        if (persistentObjectFunction == null)
        {
            persistentObjectFunction = new PersistentObjectFunction();
            persistentObjectFunction.identifier = info.identifier;
            ds.InsertObjectFunction(persistentObjectFunction);
        }
        else
        {
            info.hasViewed = persistentObjectFunction.isViewed;
            info.hasViewed = true;
        }
    }

    void ViewFunction(string identifier)
    {
        //persistentObjectFunction.amount = amount;
        //currencyAmountByIdentifier[id] = currency.amount;
        //ds.UpdateCurrencyAmount(currency);
    }
}

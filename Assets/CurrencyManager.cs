using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class CurrencyManager : Singleton<CurrencyManager> {



    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    public DataService ds;
    public void Init()
    {
        ds = new DataService("db.s3db");
        var currency = ds.GetPersistentCurrencys();
    }

    public void Updatevalue(string id, int amount)
    {
        PersistentCurrency currency = ds.GetPointAmount();
        currency.amount = amount;
        ds.UpdatePointAmount(currency);
    }



    public int AmountOfCurrency(string currencyId)
    {
        return ds.GetPointAmount().amount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class CurrencyManager : Singleton<CurrencyManager> {
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    public DataService ds;
    Dictionary<string, int> currencyAmountByIdentifier;
    public void Init()
    {
        ds = new DataService("db.s3db");
        var currency = ds.GetPersistentCurrencys();
        currencyAmountByIdentifier = new Dictionary<string, int>();
        currencyAmountByIdentifier["points"] = ds.GetCurrencyAmount("points").amount;
    }

    public void UpdateValue(string id, int amount)
    {
        PersistentCurrency currency = ds.GetCurrencyAmount(id);
        currency.amount = amount;
        currencyAmountByIdentifier[id] = currency.amount;
        ds.UpdateCurrencyAmount(currency);
    }

    public void AddValue(string id,int amount)
    {
        PersistentCurrency currency = ds.GetCurrencyAmount(id);
        currency.amount += amount;
        currencyAmountByIdentifier[id] = currency.amount;
        ds.UpdateCurrencyAmount(currency);
    }

    public int AmountOfCurrency(string currencyId)
    {
        return currencyAmountByIdentifier[currencyId];
    }
}

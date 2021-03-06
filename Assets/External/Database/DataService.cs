﻿using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){
#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
        
	var loadDb =  Application.streamingAssetsPath+"/"+ DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        //Debug.LogError(Application.streamingAssetsPath);
        //var loadDb = Application.streamingAssetsPath+ DatabaseName; ;
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

    //public void CreateDB(){
    //	_connection.DropTable<Person> ();
    //	_connection.CreateTable<Person> ();

    //	_connection.InsertAll (new[]{
    //		new Person{
    //			Id = 1,
    //			Name = "Tom",
    //			Surname = "Perez",
    //			Age = 56
    //		},
    //		new Person{
    //			Id = 2,
    //			Name = "Fred",
    //			Surname = "Arthurson",
    //			Age = 16
    //		},
    //		new Person{
    //			Id = 3,
    //			Name = "John",
    //			Surname = "Doe",
    //			Age = 25
    //		},
    //		new Person{
    //			Id = 4,
    //			Name = "Roberto",
    //			Surname = "Huertas",
    //			Age = 37
    //		}
    //	});
    //}

    public IEnumerable<PersistentCurrency> GetPersistentCurrencys()
    {
        return _connection.Table<PersistentCurrency>();
    }

    public PersistentCurrency GetCurrencyAmount(string identifier)
    {
        return _connection.Table<PersistentCurrency>().Where(x => x.identifier == identifier).FirstOrDefault();
    }

    public void UpdateCurrencyAmount(PersistentCurrency currency)
    {
        _connection.Update(currency);
    }

    public IEnumerable<PersistentObjectFunction> GetAllObjectFunctions()
    {
        return _connection.Table<PersistentObjectFunction>();
    }
    public PersistentObjectFunction GetPersistentObjectFunction(string identifier)
    {
        return _connection.Table<PersistentObjectFunction>().Where(x => x.identifier == identifier).FirstOrDefault();
    }

    public void InsertObjectFunction(PersistentObjectFunction achieve)
    {
        _connection.Insert(achieve);
    }

    public void UpdateObjectFunction(PersistentObjectFunction achieve)
    {
        _connection.Update(achieve);
    }

    public void DeleteAllObjectFunction()
    {
        _connection.DeleteAll<PersistentObjectFunction>();
    }


    public IEnumerable<PersistentAchievement> GetAllAchievements()
    {
        return _connection.Table<PersistentAchievement>();
    }

    public PersistentAchievement GetPersistentAchievement(string identifier)
    {
        return _connection.Table<PersistentAchievement>().Where(x => x.identifier == identifier).FirstOrDefault();
    }

    public void InsertAchievement(PersistentAchievement achieve)
    {
        _connection.Insert(achieve);
    }

    public void UpdateAchievement(PersistentAchievement achieve)
    {
        _connection.Update(achieve);
    }

    public PersistentAchievementAmountStep GetPersistentAchievementAmountStep(string identifier)
    {
        return _connection.Table<PersistentAchievementAmountStep>().Where(x => x.identifier == identifier).FirstOrDefault();
    }

    public void InsertAchievementAmountStep(PersistentAchievementAmountStep achieve)
    {
        _connection.Insert(achieve);
    }

    public void UpdateAchievementAmountStep(PersistentAchievementAmountStep achieve)
    {
        _connection.Update(achieve);
    }



    //public IEnumerable<Person> GetPersonsNamedRoberto()
    //{
    //    return _connection.Table<Person>().Where(x => x.Name == "Roberto");
    //}

    //public Person GetJohnny(){
    //	return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
    //}

    //public Person CreatePerson(){
    //	var p = new Person{
    //			Name = "Johnny",
    //			Surname = "Mnemonic",
    //			Age = 21
    //	};
    //	_connection.Insert (p);
    //	return p;
    //}
}

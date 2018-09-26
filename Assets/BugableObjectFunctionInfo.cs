using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableObjectFunctionInfo {
    public string identifier;
    public string description;
    public string objectId;
    public string prerequisite;
    public string achievementToFinish;
    public int indexInObject;

    //objectId	description	prerequisite	achievementToFinish	indexInObject

    //public Sprite icon {
    //    get
    //    {
    //        MonsterEnum monsterEnum = (MonsterEnum)System.Enum.Parse(typeof(MonsterEnum), identifier);
    //        return ResourceManager.Instance.monsterSprite[(int)monsterEnum];
    //    }

    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelationDescriptionEnum {dontKnow, notFamiliar,talkedSeveralTimes,sitAround,playedTogether,friend,bestFriend, lover, }
public enum HealthDescriptionEnum { healthy, weak, hurt, dying, dead }
public enum AwarenessEnum { dontcare,normal,aware,nervous }


public class HumanInfo : MonoBehaviour
{
    public int nameId;
    public bool isBoy;
    public int sightRange;
    public RelationDescriptionEnum relationDescriptionEnum;
    public HealthDescriptionEnum healthDescriptionEnum;
    public AwarenessEnum awarenessEnum;
    // Start is called before the first frame update
    void Start()
    {
        RandomInfo();
    }

    public string Name()
    {
        return isBoy ? EnumParser.boysNames[nameId]: EnumParser.girlsNames[nameId];
    }
    public string SubjectiveProunoun(bool needCapitalize = false)
    {
        if (needCapitalize)
        {
            return isBoy ? "He" : "She";
        }
        else
        {
            return isBoy ? "he" : "she";
        }
    }

    public string PosseciveProunoun(bool needCapitalize = false)
    {
        if (needCapitalize)
        {
            return isBoy ? "His" : "Her";
        }
        else
        {
            return isBoy ? "he" : "her";
        }
    }

    public string ObjectiveProunoun(bool needCapitalize = false)
    {
        if (needCapitalize)
        {
            return isBoy ? "Him" : "Her";
        }
        else
        {
            return isBoy ? "him" : "her";
        }
    }

    void RandomInfo()
    {
        isBoy = Random.Range(0, 2)>0;
        nameId = Random.Range(0, isBoy ? EnumParser.boysNames.Length : EnumParser.girlsNames.Length);
        relationDescriptionEnum = (RelationDescriptionEnum)Random.Range(0, System.Enum.GetValues(typeof(RelationDescriptionEnum)).Length);
        healthDescriptionEnum = HealthDescriptionEnum.healthy;
        awarenessEnum = (AwarenessEnum)Random.Range(0, System.Enum.GetValues(typeof(AwarenessEnum)).Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

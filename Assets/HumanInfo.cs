using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelationDescriptionEnum {dontKnow, notFamiliar,talkedSeveralTimes,sitAround,playedTogether,friend,bestFriend, lover, }
public enum HealthDescriptionEnum { healthy, hurt, dying, dead }
public enum AwarenessEnum { dontcare,normal,aware,nervous }


public class HumanInfo : MonoBehaviour
{
    public int nameId;
    public bool isBoy;
    public int sightRange;
    public RelationDescriptionEnum relationDescriptionEnum;
    public HealthDescriptionEnum healthDescriptionEnum;
    public int hp = 100;
    public AwarenessEnum awarenessEnum;
    public int AttackChance;
    public int DodgeChance;
    public int RunawayChance;
    public int CoopChance;
    public int HowYouBehaveInTheGame;
    public int HowOthersBehaveInTheGame;
    public bool hasBeenAttackedByMe;
    public bool hasBeenAttackedByAnyone;
    public bool isWeak;
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
        HowYouBehaveInTheGame = 0;//-5 when you kill someone, -10 when you kill his bf or lover.  -50 when you try to attack him,
        //+5 when you talk or him, +10 when you provide help
        HowOthersBehaveInTheGame = 0; //-1 when anyone kill some one, -10 when someone kiil his bf or lover, -10 when someone try to kill him
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

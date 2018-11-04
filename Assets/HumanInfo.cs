using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelationDescriptionEnum {dontKnow, notFamiliar,talkedSeveralTimes,sitAround,playedTogether,friend,bestFriend, lover, }
public enum HealthDescriptionEnum { healthy, hurt, dying, dead }
public enum AwarenessEnum { dontcare,normal,aware,nervous }


public class HumanInfo : MonoBehaviour
{
    public bool isBoy;
    public int sightRange=3;
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
    public string bestFriendName;
    public string loverName;
    public List<InventoryEnum> inventories;

    public HumanInfo targetHumanInfo;
    public Transform targetTransform;
    public int remainTargetTransformMovement;

    public SpriteRenderer sr;
    public Shader greyScaleShader;
    // Start is called before the first frame update
    public void Init()
    {
        inventories = new List<InventoryEnum>();
        inventories.Add(InventoryEnum.stone);
        sr = GetComponent<SpriteRenderer>();
        RandomInfo();
    }

    public string Name;
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
        //isBoy = Random.Range(0, 2)>0;
        //nameId = Random.Range(0, isBoy ? EnumParser.boysNames.Length : EnumParser.girlsNames.Length);
        //relationDescriptionEnum = (RelationDescriptionEnum)Random.Range(0, System.Enum.GetValues(typeof(RelationDescriptionEnum)).Length);
        //healthDescriptionEnum = HealthDescriptionEnum.healthy;
        //awarenessEnum = (AwarenessEnum)Random.Range(0, System.Enum.GetValues(typeof(AwarenessEnum)).Length);
        //HowYouBehaveInTheGame = 0;//-5 when you kill someone, -10 when you kill his bf or lover.  -50 when you try to attack him,
        ////+5 when you talk or him, +10 when you provide help
        //HowOthersBehaveInTheGame = 0; //-1 when anyone kill some one, -10 when someone kiil his bf or lover, -10 when someone try to kill him
    }

    public List<InventoryEnum> RobHuman(HumanInfo humanInfo)
    {
        List<InventoryEnum> robItems = new List<InventoryEnum>();
        foreach(InventoryEnum inventory in humanInfo.inventories)
        {
            if (!inventories.Contains(inventory))
            {
                robItems.Add(inventory);
                inventories.Add(inventory);
            }
        }
        humanInfo.inventories = new List<InventoryEnum>();
        BRInventoryViewController.Instance.UpdateInventoryView();
        return robItems;
    }

    public int SightRange()
    {
        int range = sightRange;
        if (inventories.Contains(InventoryEnum.binoculars))
        {
            range = 5;
        }
        return range;
    }

    public List<InventoryEnum> AllWeapons()
    {
        List<InventoryEnum> weapons = new List<InventoryEnum>();
        foreach(InventoryEnum inventory in inventories)
        {
            if (InventoryManager.Instance.IsWeapon(inventory))
            {
                weapons.Add(inventory);
            }
        }
        return weapons;
    }

    public void UpdateHeathyState()
    {
        if (!IsAlive)
        {
            return;
        }
        if (hp >= 20)
        {
            healthDescriptionEnum = HealthDescriptionEnum.hurt;
        }
        else if (hp > 0)
        {
            healthDescriptionEnum = HealthDescriptionEnum.dying;
        }
        else
        {
            healthDescriptionEnum = HealthDescriptionEnum.dead;
            sr.material.shader = greyScaleShader;
            ResourceManager.Instance.LeftPeople -= 1;
        }
    }

    public void HurtHuman(int damageNumber, string attacker)
    {
        if (IsAlive)
        {
            hp -= damageNumber;
            UpdateHeathyState();
            if (!IsAlive)
            {
                BRMessageViewController.Instance.AddCell(attacker, this);
            }
        }
    }

    public void Attack(HumanInfo humanInfo) 
    {
        targetHumanInfo = humanInfo;
        List<InventoryEnum> weapons = AllWeapons();
        InventoryEnum weapon = weapons[Random.Range(0, weapons.Count)];
        InventoryManager.Instance.AttackWithInventory(weapon, this, humanInfo);
    }

    public bool IsAlive { get { return healthDescriptionEnum != HealthDescriptionEnum.dead; } }

    // Update is called once per frame
    void Update()
    {
        
    }
}

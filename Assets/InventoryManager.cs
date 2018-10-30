using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryEnum { stone, kitchenKnife, shotgun, pistol, binoculars,
    megaphone, crossbow, handAxe, kevlarVest, grenade, potLid,  }
//harisen, flashlight, rope, dagger, sickle, fork, machete, switchBlade, 
public class InventoryManager : Singleton<InventoryManager>
{
    public List<InventoryEnum> inventories;
    int[] chanceFromFarAway = new int[] { 10, 20, 60, 40, 10,
        10, 50, 30, 0, 100, 10 };
    int[] damageMinFromFarAway = new int[] { 10, 15, 30, 40, 10,
        10, 20, 20, 0, 90, 10 };
    int[] damageMaxFromFarAway = new int[] { 15, 30, 100, 90, 15,
        15, 50, 40, 0, 100, 20 };
    int[] goodAtWeapon = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    int[] chanceFromClose = new int[] { 50, 60, 20, 70, 10,
        10, 30, 40, 0, 100, 100 };
    int[] damageMinFromClose = new int[] { 20, 30, 10, 60, 10,
        10, 50, 70, 0, 100, 10 };
    int[] damageMaxFromClose = new int[] { 45, 50, 100, 100, 15,
        15, 60, 80, 0, 100, 20 };
    int[] weaponItemLeft = new int[] {1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        inventories = new List<InventoryEnum>();
        inventories.Add(InventoryEnum.stone);
    }

    bool DidHit(InventoryEnum inventory, HumanInfo humanInfo)
    {
        int chance = chanceFromFarAway[(int)inventory];
        int rand = Random.Range(0, 100);
        return chance > rand;
    }


    string DamageString(HumanInfo humanInfo, InventoryEnum inventory, bool isHitOnHead)
    {
        string res = "";
        int damage = Random.Range(damageMinFromFarAway[(int)inventory], damageMaxFromFarAway[(int)inventory]);
        if (isHitOnHead)
        {
            damage *= 2;
        }
        humanInfo.hp -= damage;
        if (damage < 20)
        {
            res = "It scratched " + humanInfo.Name() + " a little. ";
        } else if(damage < 50)
        {
            res = "It hurt " + humanInfo.Name() + ". ";
        }
        else if (damage < 100)
        {
            res = "It hurt " + humanInfo.Name() + "very bad. ";
        }
        if (humanInfo.hp >= 80)
        {
            res += humanInfo.Name() + " is still very healthy";
        } else if (humanInfo.hp >= 50)
        {
            res += humanInfo.Name() + " looks not that good but "+humanInfo.PosseciveProunoun()+" wound only make him stronger. ";
        }
        else if (humanInfo.hp >= 20)
        {
            res += humanInfo.Name() + " was not good and his movement is slow now.";
        }
        switch (humanInfo.healthDescriptionEnum)
        {
            
            case HealthDescriptionEnum.healthy:

                break;
            case HealthDescriptionEnum.hurt:
                break;
            case HealthDescriptionEnum.dying:
                res = humanInfo.SubjectiveProunoun(true) + " is dying, " + humanInfo.SubjectiveProunoun()+ " can't take any damage at all. Now " + humanInfo.SubjectiveProunoun(false)+" is thoroughly dead.";
                humanInfo.hp = 0;
                break;
            case HealthDescriptionEnum.dead:
                res = humanInfo.SubjectiveProunoun(true) + " has been dead. What do you expect from attacking " + humanInfo.ObjectiveProunoun() + " again?";
                break;
        }
        if (humanInfo.hp >= 20)
        {
            humanInfo.healthDescriptionEnum = HealthDescriptionEnum.hurt;
        }
        else if (humanInfo.hp > 0)
        {
            humanInfo.healthDescriptionEnum = HealthDescriptionEnum.dying;
        } else
        {
            humanInfo.healthDescriptionEnum = HealthDescriptionEnum.dead;
        }
        return res;
    }

    string MissTalk(HumanInfo humanInfo)
    {
        string res = "";
        if (!humanInfo.hasBeenAttackedByMe)
        {
            if (!humanInfo.hasBeenAttackedByAnyone)
            {
                res = humanInfo.Name() + " is shocking and yelling at you: \n";
                switch (humanInfo.relationDescriptionEnum)
                {
                    case RelationDescriptionEnum.dontKnow:
                        res+="\"Hey man what's wrong with you!";
                        break;
                    case RelationDescriptionEnum.notFamiliar:
                        res += "I know I barely talked with you but we were still classmates right?";
                        break;
                    case RelationDescriptionEnum.talkedSeveralTimes:
                        res += "We were.. classmates right? Why do you attack me?";
                        break;
                    case RelationDescriptionEnum.sitAround:
                        res += "Are you lossing your mind? You copied my homework yesterday! ";
                        break;
                    case RelationDescriptionEnum.playedTogether:
                        res += "What's wrong with you? We were playing football together yesterday! ";
                        break;
                    case RelationDescriptionEnum.friend:
                        res += "Look at me I'm " + humanInfo.Name() + "! We were friends!";
                        break;
                    case RelationDescriptionEnum.bestFriend:
                        res += "I trusted you you were my best friend!, I ..";
                        break;
                    case RelationDescriptionEnum.lover:
                        res += "I guess love means nothing to you.";
                        break;
                }
            }
        }
        else
        {
            if (humanInfo.healthDescriptionEnum == HealthDescriptionEnum.dying || humanInfo.hp < 30)
            {
                res = humanInfo.Name() + " is begging you: \n";
                string[] strings;
                switch (humanInfo.relationDescriptionEnum)
                {
                    case RelationDescriptionEnum.dontKnow:
                    case RelationDescriptionEnum.notFamiliar:
                    case RelationDescriptionEnum.talkedSeveralTimes:
                        strings = new string[] { "Please Don't kill me, I, I don't want to die. ",
                        };
                        res += strings[Random.Range(0, strings.Length - 1)];
                        break;
                    case RelationDescriptionEnum.sitAround:
                        strings = new string[] { "Please Don't kill me.. Remember the good days we had before? We were friends!",
                        };
                        res += strings[Random.Range(0, strings.Length - 1)];
                        break;
                    case RelationDescriptionEnum.playedTogether:
                        res += "Please Don't kill me.. Remember the good days we had before? We were friends! ";
                        break;
                    case RelationDescriptionEnum.friend:
                        res += "Please Don't kill me.. Remember the good days we had before? We were best friends! ";
                        break;
                    case RelationDescriptionEnum.bestFriend:
                        res += "Do you really want to kill me? I thought I would protect you and you would protect me, I didn't think you are the person to kill me. I deserve it I think. ";
                        break;
                    case RelationDescriptionEnum.lover:
                        strings = new string[] { "I love you, from the first time I met you, don't do this to me. ", "Do you remember the first time we met? You were looking at me stupidly and rode your bike into the pond. But I thought you were cute. " };
                        //string[] strings = new string[] { "I HATE YOU. ", "You will regret.", "I curse you for the rest of my life and I'll haunted on you until you die.", };
                        res += strings[Random.Range(0, strings.Length - 1)];
                        break;
                }
            }
        }
        return res;
        }

    public void UseInventory(InventoryEnum inventory,HumanInfo humanInfo)
    {
        List<string> dialogs = new List<string>();
        switch (inventory)
        {
            case InventoryEnum.stone:
                dialogs.Add("You throw a stone to " + humanInfo.Name());
                bool didHit = DidHit(inventory,humanInfo);
                if (didHit)
                {
                    bool isHitOnHead = Random.Range(0, 100) > 80;
                    dialogs.Add("It hit " + humanInfo.Name() +" on "+humanInfo.PosseciveProunoun() + (isHitOnHead?" head.":" body "));
                    dialogs.Add(DamageString(humanInfo,inventory,isHitOnHead));
                    dialogs.Add(MissTalk(humanInfo));
                }else
                {
                    dialogs.Add("It missed!");

                    dialogs.Add(MissTalk(humanInfo));
                }
                break;
            case InventoryEnum.kitchenKnife:
                break;
            case InventoryEnum.shotgun:
                break;
            case InventoryEnum.pistol:
                break;
            case InventoryEnum.binoculars:
                break;
            case InventoryEnum.megaphone:
                break;
            case InventoryEnum.crossbow:
                break;
            case InventoryEnum.handAxe:
                break;
            case InventoryEnum.kevlarVest:
                break;
            case InventoryEnum.grenade:
                break;
            case InventoryEnum.potLid:
                break;
        }
        DialogManager.CreateViewController(dialogs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

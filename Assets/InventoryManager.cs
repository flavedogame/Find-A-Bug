using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryEnum { stone, kitchenKnife, shotgun, pistol, binoculars,
    megaphone, crossbow, handAxe, kevlarVest, grenade, potLid,  }
//harisen, flashlight, rope, dagger, sickle, fork, machete, switchBlade, baseball bat, jutte, semi-automatic shotgun, katana, potassium cyanide,
//submachine gun, gps tracking device, sickle, nunchaku,pickaxe,revolver
public class InventoryManager : Singleton<InventoryManager>
{
    public List<InventoryEnum> inventories;
    int[] chanceToHit = new int[] { 10, 90, 60, 40, 0,
        0, 50, 40, 0, 100, 20 };
    int[] damageMin = new int[] { 10, 30, 30, 40, 0,
        0, 50, 40, 0, 90, 5 };
    int[] damageMax = new int[] { 100, 40, 100, 90, 15,
        0, 50, 90, 0, 100, 100 };
    int[] goodAtWeapon = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    int[] weaponRange = new int[] { 3, 1, 7, 5, 0,
        0, 3, 2, 0, 100, 1 };

    public bool IsWeapon(InventoryEnum inventory)
    {
        return weaponRange[(int)inventory] > 0;
    }

    public void AttackWithInventory(InventoryEnum weapon,HumanInfo attacker,HumanInfo attackee)
    {
        switch (weapon)
        {
            case InventoryEnum.shotgun:
            case InventoryEnum.pistol:
                BRHintManager.Instance.AddHint(attacker.transform.position, BRHintEnum.gun);
                break;
            default:
                break;
        }
        if (DidHit(weapon, attackee))
        {
            bool isHitOnHead = Random.Range(0, 100) > 80;
            int damage = Random.Range(damageMin[(int)weapon], damageMax[(int)weapon]);
            if (isHitOnHead)
            {
                damage *= 2;
            }
            attackee.hp -= damage;
            attackee.UpdateHeathyState();
            if (attackee.hp<=0)
            {
                BRMessageViewController.Instance.AddCell(attacker, attackee);
            }
        }
        //make noise

    }
    // Start is called before the first frame update
    void Start()
    {
        inventories = new List<InventoryEnum>();
        inventories.Add(InventoryEnum.stone);
    }

    public bool IsInventoryUsable(InventoryEnum inventoryEnum, HumanInfo attackee)
    {
        HumanInfo heroInfo = HumanManager.Instance.heroInfo;
        if (attackee != heroInfo)
        {
            if (Vector3.Distance( attackee.transform.position, heroInfo.transform.position) <= weaponRange[(int)inventoryEnum]){
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    bool DidHit(InventoryEnum inventory, HumanInfo attackee)
    {
        int chance = chanceToHit[(int)inventory];
        int rand = Random.Range(0, 100);
        return chance > rand;
    }


    string DamageString(HumanInfo humanInfo, InventoryEnum inventory, bool isHitOnHead)
    {
        string res = "";
        int damage = Random.Range(damageMin[(int)inventory], damageMax[(int)inventory]);
        if (isHitOnHead)
        {
            damage *= 2;
        }
        humanInfo.hp -= damage;
        if (damage < 20)
        {
            res = "It scratched " + humanInfo.Name+ " a little. ";
        } else if(damage < 50)
        {
            res = "It hurt " + humanInfo.Name + ". ";
        }
        else if (damage < 100)
        {
            res = "It hurt " + humanInfo.Name + "very bad. ";
        }
        if (humanInfo.hp >= 80)
        {
            res += humanInfo.Name + " is still very healthy";
        } else if (humanInfo.hp >= 50)
        {
            res += humanInfo.Name+ " looks not that good but "+humanInfo.PosseciveProunoun()+" wound only make him stronger. ";
        }
        else if (humanInfo.hp >= 20)
        {
            res += humanInfo.Name + " was not good and his movement is slow now.";
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
        humanInfo.UpdateHeathyState();
        return res;
    }

    string MissTalk(HumanInfo humanInfo)
    {
        string res = "";
        if (!humanInfo.hasBeenAttackedByMe)
        {
            if (!humanInfo.hasBeenAttackedByAnyone)
            {
                res = humanInfo.Name + " is shocking and yelling at you: \n";
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
                        res += "Look at me I'm " + humanInfo.Name + "! We were friends!";
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
                res = humanInfo.Name+ " is begging you: \n";
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

    void UseWeapon(List<string> dialogs,InventoryEnum inventory, HumanInfo humanInfo)
    {
        switch (inventory)
        {
            case InventoryEnum.stone:
                dialogs.Add("You throwed a stone to " + humanInfo.Name);
                break;
            case InventoryEnum.kitchenKnife:
                dialogs.Add("You thrust a kitchen knife to " + humanInfo.Name);
                break;
            case InventoryEnum.shotgun:
                dialogs.Add("You shot " + humanInfo.Name+" with a shotgun.");
                break;
            case InventoryEnum.pistol:
                dialogs.Add("You shot " + humanInfo.Name + " with a pistol.");
                break;
            case InventoryEnum.grenade:
                dialogs.Add("You throw a grenade to " + humanInfo.Name);
                break;
            case InventoryEnum.potLid:
                dialogs.Add("You hit " + humanInfo.Name+" with a pot lid.");
                break;
            case InventoryEnum.crossbow:
                dialogs.Add("You shot " + humanInfo.Name + " with a crossbow.");
                break;
            case InventoryEnum.handAxe:
                dialogs.Add("You attack " + humanInfo.Name + " with a hand axe.");
                break;
        }
                
        bool didHit = DidHit(inventory, humanInfo);
        if (didHit)
        {
            bool isHitOnHead = Random.Range(0, 100) > 80;
            dialogs.Add("It hit " + humanInfo.Name + " on " + humanInfo.PosseciveProunoun() + (isHitOnHead ? " head." : " body "));
            dialogs.Add(DamageString(humanInfo, inventory, isHitOnHead));
            dialogs.Add(MissTalk(humanInfo));
        }
        else
        {
            dialogs.Add("It missed!");

            dialogs.Add(MissTalk(humanInfo));
        }
    }

    public void UseInventory(InventoryEnum inventory,HumanInfo humanInfo)
    {
        List<string> dialogs = new List<string>();
        switch (inventory)
        {
            case InventoryEnum.stone:
            case InventoryEnum.kitchenKnife:
            case InventoryEnum.shotgun:
            case InventoryEnum.pistol:
            case InventoryEnum.grenade:
            case InventoryEnum.potLid:
            case InventoryEnum.crossbow:
            case InventoryEnum.handAxe:
                UseWeapon(dialogs, inventory, humanInfo);
                break;

            case InventoryEnum.kevlarVest:
                break;
            case InventoryEnum.binoculars:
                break;
            case InventoryEnum.megaphone:
                break;
        }
        DialogManager.CreateViewController(dialogs);
    }

    static public string InventoryName(InventoryEnum inventoryEnum)
    {
        switch (inventoryEnum)
        {
            case InventoryEnum.stone:
                return "Stone";
                break;
            case InventoryEnum.kitchenKnife:
                return "Kitchen Knife";
                break;
            case InventoryEnum.shotgun:
                return "Shotgun";
                break;
            case InventoryEnum.pistol:
                return "Pistol";
                break;
            case InventoryEnum.binoculars:
                return "Binocular";
                break;
            case InventoryEnum.megaphone:
                return "Megaphone";
                break;
            case InventoryEnum.crossbow:
                return "Crossbow";
                break;
            case InventoryEnum.handAxe:
                return "Hand Axe";
                break;
            case InventoryEnum.kevlarVest:
                return "Kevlar Vest";
                break;
            case InventoryEnum.grenade:
                return "Grenade";
                break;
            case InventoryEnum.potLid:
                return "Potlid";
                break;
        }
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

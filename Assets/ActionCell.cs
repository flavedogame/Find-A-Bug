using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ActionEnum { talk,takeItem }

public class ActionCell : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Button actionButton;
    HumanStateViewController viewController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void TakeItem(HumanInfo humanInfo)
    {
        List<string> dialogs = new List<string>();
        
        switch (humanInfo.healthDescriptionEnum)
        {
            case HealthDescriptionEnum.healthy:
            case HealthDescriptionEnum.hurt:
                bool robSuccessful = Random.Range(0, 10) > 5;
                if (robSuccessful)
                {
                    List<InventoryEnum> list2 = HumanManager.Instance.heroInfo.RobHuman(humanInfo);
                    if (list2.Count == 0)
                    {
                        dialogs.Add("You looked into " + humanInfo.Name + "'s bag but find nothing. ");
                        dialogs.Add(humanInfo.Name + " noticed and grab "+humanInfo.PosseciveProunoun()+" bag back and shouted:");
                        dialogs.Add("\"How dare you do this! \"");
                        humanInfo.HowYouBehaveInTheGame -= 30;
                    }
                    else
                    {
                        string listString = "";
                        foreach (InventoryEnum inventory in list2)
                        {
                            if (listString.Length != 0)
                            {
                                listString += ", ";
                            }
                            listString += InventoryManager.InventoryName(inventory);
                        }
                        dialogs.Add("You looked into " + humanInfo.Name + "'s bag and find " + listString + ".");
                        if (list2.Contains(InventoryEnum.binoculars))
                        {
                            dialogs.Add("Binoculars make you see farther.");
                        }
                        dialogs.Add(humanInfo.Name + " noticed and grab " + humanInfo.PosseciveProunoun() + " bag back and shouted:");
                        dialogs.Add("\"Give back my inventories! We are enemies now! \"");
                        humanInfo.HowYouBehaveInTheGame -= 100;
                    }
                }
                else
                {
                    dialogs.Add(humanInfo.Name + " shrank back and stared at you: ");
                    dialogs.Add("\"What are you trying to do? Steal my inventory?\"");
                    humanInfo.HowYouBehaveInTheGame -= 10;
                }
                break;
            case HealthDescriptionEnum.dying:
                List<InventoryEnum> list = HumanManager.Instance.heroInfo.RobHuman(humanInfo);
                if (list.Count == 0)
                {
                    dialogs.Add("You looked into " + humanInfo.Name + "'s bag but find nothing. ");
                    dialogs.Add(humanInfo.Name + " squint at you and sneer:");
                    dialogs.Add("\"Quite dispointed right? You should be thankful, if I have any inventories, you would die. \"");
                } else
                {
                    string listString = "";
                    foreach (InventoryEnum inventory in list)
                    {
                        if (listString.Length != 0)
                        {
                            listString += ", ";
                        }
                        listString += InventoryManager.InventoryName(inventory);
                    }
                    dialogs.Add("You looked into " + humanInfo.Name + "'s bag and find "+listString+".");
                    if (list.Contains(InventoryEnum.binoculars))
                    {
                        dialogs.Add("Binoculars make you see farther.");
                    }
                    dialogs.Add(humanInfo.Name + " wanted to beg you, "+humanInfo.SubjectiveProunoun() +" opened "+humanInfo.PosseciveProunoun() +" mouth but only sighed.");
                }
                break;
            case HealthDescriptionEnum.dead:
                list = HumanManager.Instance.heroInfo.RobHuman(humanInfo);
                if (list.Count == 0)
                {
                    dialogs.Add("You looked into " + humanInfo.Name + "'s bag but find nothing. ");
                }
                else
                {
                    string listString = "";
                    foreach (InventoryEnum inventory in list)
                    {
                        if (listString.Length != 0)
                        {
                            listString += ", ";
                        }
                        listString += InventoryManager.InventoryName(inventory);
                    }
                    dialogs.Add("You looked into " + humanInfo.Name + "'s bag and find " + listString + ".");
                    if (list.Contains(InventoryEnum.binoculars))
                    {
                        dialogs.Add("Binoculars make you see farther.");
                    }
                }
                break;
        }
        DialogManager.CreateViewController(dialogs);
    }

    public void InitCell(ActionEnum action, HumanInfo humanInfo, HumanStateViewController controller)
    {
        viewController = controller;
        switch (action)
        {
            case ActionEnum.talk:
                description.text = "Talk";
                actionButton.onClick.AddListener(delegate {
                    DoAction();
                });
                break;
            case ActionEnum.takeItem:
                description.text = "Take His Inventory";
                actionButton.onClick.AddListener(delegate {
                    TakeItem(humanInfo);
                    DoAction();
                });
                break;
        }
    }

    void DoAction()
    {
        TurnBaseClock.Instance.UpdateTime();
        viewController.Back();
    }

    public void InitCell(InventoryEnum inventory, HumanInfo humanInfo,HumanStateViewController controller)
    {
        viewController = controller;
        switch (inventory)
        {
            case InventoryEnum.stone:
                description.text = "Throw a stone";
                break;
            case InventoryEnum.kitchenKnife:
                description.text = "Thrust a kitchen knife";
                break;
            case InventoryEnum.shotgun:
                description.text = "Shoot shotgun";
                break;
            case InventoryEnum.pistol:
                description.text = "Shoot pistol";
                break;
            case InventoryEnum.binoculars:
                description.text = "Throw a binoculars";
                break;
            case InventoryEnum.megaphone:
                description.text = "Throw a megaphone";
                break;
            case InventoryEnum.crossbow:
                description.text = "Shoot crossbow";
                break;
            case InventoryEnum.handAxe:
                description.text = "Wield a handAxe";
                break;
            case InventoryEnum.kevlarVest:
                description.text = "Throw a kevlarVest";
                break;
            case InventoryEnum.grenade:
                description.text = "Throw a grenade";
                break;
            case InventoryEnum.potLid:
                description.text = "Wield a potLid";
                break;
        }


        actionButton.onClick.AddListener(delegate {
            InventoryManager.Instance.UseInventory(inventory, humanInfo);
            DoAction();
        });

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

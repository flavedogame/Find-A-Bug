using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ActionEnum { talk, team, takeItem }
public enum ActionSelfEnum { rest, suicide, practice }

public class ActionCell : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Button actionButton;
    HumanStateViewController viewController;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Talk(HumanInfo humanInfo)
    {
        List<string> dialogs = new List<string>();
        string[] talks = new string[] { "You talked about what do you think about this 'game'",
            "You talked about your fearness.",
            "You talked about some jokes and you don't that scary now.",
            "You talked about the people you care.",
            "You talked about the anger of you.",
            "You talked about how you miss your bed and hot bath.",
            "You talked about .. the weather.",
            };
        dialogs.Add(talks[Random.Range(0, talks.Length)]);
        dialogs.Add(humanInfo.Name + " thinks you are a good person.");
        humanInfo.HowYouBehaveInTheGame += Random.Range(5, 15);
        DialogManager.CreateViewController(dialogs);
    }

    void Team(HumanInfo humanInfo)
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You asked " + humanInfo.Name + " to team with you.");
        int[] TeamWithRelationship = new int[] { 30, 40, 50, 50, 60, 70, 80, 90 };
        if (TeamWithRelationship[(int)humanInfo.relationDescriptionEnum] + humanInfo.HowYouBehaveInTheGame >= 100)
        {
            dialogs.Add(humanInfo.SubjectiveProunoun(true) + " agreed. " + humanInfo.SubjectiveProunoun(true) + " promised " + humanInfo.SubjectiveProunoun() + " wont attack you, until you attack " + humanInfo.ObjectiveProunoun() + " first.");
            humanInfo.isTeamed = true;
        }
        else
        {
            dialogs.Add(humanInfo.SubjectiveProunoun(true) + " disagreed. " + humanInfo.SubjectiveProunoun(true) + " doesn't trust you enough, maybe talk to " + humanInfo.ObjectiveProunoun() + " more.");
        }
        humanInfo.HowYouBehaveInTheGame += Random.Range(0, 10);
        dialogs.Add(humanInfo.Name + " thinks you are a good person.");
        DialogManager.CreateViewController(dialogs);
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
                        dialogs.Add(humanInfo.Name + " noticed and grab " + humanInfo.PosseciveProunoun() + " bag back and shouted:");
                        dialogs.Add("\"How dare you do this! \"");
                        humanInfo.HowYouBehaveInTheGame -= 30;
                        if (humanInfo.isTeamed)
                        {
                            dialogs.Add(humanInfo.Name + " and you are not teamed anymore.");
                            humanInfo.isTeamed = false;
                        }
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
                        if (humanInfo.isTeamed)
                        {
                            dialogs.Add(humanInfo.Name + " and you are not teamed anymore.");
                            humanInfo.isTeamed = false;
                        }
                    }
                }
                else
                {
                    dialogs.Add(humanInfo.Name + " shrank back and stared at you: ");
                    dialogs.Add("\"What are you trying to do? Steal my inventory?\"");
                    humanInfo.HowYouBehaveInTheGame -= 10;
                    if (humanInfo.isTeamed)
                    {
                        dialogs.Add(humanInfo.Name + " and you are not teamed anymore.");
                        humanInfo.isTeamed = false;
                    }
                }
                break;
            case HealthDescriptionEnum.dying:
                List<InventoryEnum> list = HumanManager.Instance.heroInfo.RobHuman(humanInfo);
                if (list.Count == 0)
                {
                    dialogs.Add("You looked into " + humanInfo.Name + "'s bag but find nothing. ");
                    dialogs.Add(humanInfo.Name + " squint at you and sneer:");
                    dialogs.Add("\"Quite dispointed right? You should be thankful, if I have any inventories, you would die. \"");
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
                    dialogs.Add(humanInfo.Name + " wanted to beg you, " + humanInfo.SubjectiveProunoun() + " opened " + humanInfo.PosseciveProunoun() + " mouth but only sighed.");
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

    void Rest()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You take some rest and feels better now.");
        HumanManager.Instance.heroInfo.hp += 20;
        HumanManager.Instance.heroInfo.UpdateHeathyState();
        DialogManager.CreateViewController(dialogs);
    }

    void Suicide()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("No, why. Live is much better than die. ");
        dialogs.Add("You don't have to kill others, and you don't have to kill yourself.");
        DialogManager.CreateViewController(dialogs);
    }

    void Practice()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You practice with the inventories you got.");
        dialogs.Add("You know more about how to use them now.");
        DialogManager.CreateViewController(dialogs);
    }

    public void InitCell(ActionSelfEnum action, HumanStateViewController controller)
    {
        viewController = controller;
        switch (action)
        {
            case ActionSelfEnum.rest:
                description.text = "Rest";
                actionButton.onClick.AddListener(delegate
                {
                    Rest();
                    DoAction();
                });
                break;
            case ActionSelfEnum.suicide:
                description.text = "Suicide";
                actionButton.onClick.AddListener(delegate
                {
                    Suicide();
                    DoAction();
                });
                break;
            case ActionSelfEnum.practice:
                description.text = "Practice";
                actionButton.onClick.AddListener(delegate
                {
                    Practice();
                    DoAction();
                });
                break;
        }
    }

    public void InitCell(ActionEnum action, HumanInfo humanInfo, HumanStateViewController controller)
    {
        viewController = controller;
        switch (action)
        {
            case ActionEnum.talk:
                description.text = "Talk";
                actionButton.onClick.AddListener(delegate
                {
                    Talk(humanInfo);
                    DoAction();
                });
                break;
            case ActionEnum.team:
                description.text = "Team";
                actionButton.onClick.AddListener(delegate
                {
                    Team(humanInfo);
                    DoAction();
                });
                break;
            case ActionEnum.takeItem:
                description.text = "Take His Inventory";
                actionButton.onClick.AddListener(delegate
                {
                    TakeItem(humanInfo);
                    DoAction();
                });
                break;
        }
    }

    void DoAction()
    {
        TurnBaseClock.Instance.UpdateTime();
        OtherHumanManager.Instance.OtherHuamnMove();
        viewController.Back();
    }

    public void InitCell(InventoryEnum inventory, HumanInfo humanInfo, HumanStateViewController controller)
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



        actionButton.onClick.AddListener(delegate
        {
            InventoryManager.Instance.UseInventory(inventory, humanInfo);
            DoAction();
        });

    }


    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HumanStateViewController : DefaultViewController { 
    public TextMeshProUGUI description;
    HumanInfo humanInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Back()
    {
        base.Back();
        BRDialogManager.Instance.CloseDialog();
    }

    static public void CreateViewController(HumanInfo info)
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[7];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        HumanStateViewController script = go.GetComponent<HumanStateViewController>();
        script.Init(info);
        BRDialogManager.Instance.AddDialog();
    }

    void Init(HumanInfo info)
    {

        humanInfo = info;
        description.text = FormatDescription();
        if (humanInfo == HumanManager.Instance.heroInfo)
        {
            AddActionsForMyself();
        } else
        {
            AddActions();
        }
    }

    string FormatDescription()
    {
        string res = "";
        if (humanInfo == HumanManager.Instance.heroInfo)
        {
            res += "This is YOU, " + humanInfo.Name;
        }
        else
        {
            res += "That is " + humanInfo.Name + ". ";
        }
        if (humanInfo != HumanManager.Instance.heroInfo)
            res += RelationDesc();
        res += HealthDesc();
        if (humanInfo != HumanManager.Instance.heroInfo)
            res += GameBehaviorString();
        return res;
    }

    string GameBehaviorString()
    {
        string res = "";
        if (humanInfo.HowYouBehaveInTheGame > 0)
        {
            res = humanInfo.SubjectiveProunoun(true) + " seems trust you for what you've done before in the game.";
        } else if(humanInfo.HowYouBehaveInTheGame == 0)
        {
            res = "You haven't communicated enough since this game started.";
        } else
        {
            res = "What you have done before in thei game irritated " + humanInfo.ObjectiveProunoun();
        }
        return res;
    }

    string HealthDesc()
    {
        string res = "";
        switch (humanInfo.healthDescriptionEnum)
        {
            case HealthDescriptionEnum.healthy:
                res = humanInfo.SubjectiveProunoun(true) + " looks healthy. ";
                break;
            case HealthDescriptionEnum.hurt:
                res = humanInfo.SubjectiveProunoun(true) + " is injured. ";
                break;
            case HealthDescriptionEnum.dying:
                res = humanInfo.SubjectiveProunoun(true) + " is dying. ";
                break;
            case HealthDescriptionEnum.dead:
                res = humanInfo.SubjectiveProunoun(true) + " is dead. ";
                break;
        }
        return res;
    }

    string RelationDesc()
    {
        string res = "";
        switch (humanInfo.relationDescriptionEnum)
        {
            case RelationDescriptionEnum.dontKnow:
                res = "I don't know " + humanInfo.ObjectiveProunoun() + ". ";
                break;
            case RelationDescriptionEnum.notFamiliar:
                res = "I'm not familiar with " + humanInfo.ObjectiveProunoun()+ ". ";
                break;
            case RelationDescriptionEnum.talkedSeveralTimes:
                res = "I talked with " + humanInfo.ObjectiveProunoun() + "several times, that all. ";
                break;
            case RelationDescriptionEnum.sitAround:
                res = humanInfo.SubjectiveProunoun(true) + " sat next to me. We complained about out teacher in the class together. ";
                break;
            case RelationDescriptionEnum.playedTogether:
                res = "We played together. "+ humanInfo.SubjectiveProunoun(true)+ " is a football player as good as me. ";
                break;
            case RelationDescriptionEnum.friend:
                res = "We were good friends. ";
                break;
            case RelationDescriptionEnum.bestFriend:
                res = humanInfo.SubjectiveProunoun(true) + " was my best friend. ";
                break;
            case RelationDescriptionEnum.lover:
                res = "I love " + humanInfo.ObjectiveProunoun() + ". ";
                break;
        }
        return res;
    }
    public GameObject inventoryCell;
    public GameObject inventoryListPanel;

    void AddActionsForMyself() {
        foreach (ActionSelfEnum action in System.Enum.GetValues(typeof(ActionSelfEnum)))
        {
                GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
                ActionCell script = go.GetComponent<ActionCell>();
                script.InitCell(action, this);
           
        }
    }


    void AddActions()
    {
        foreach (ActionEnum action in System.Enum.GetValues(typeof(ActionEnum)))
        {
            if (ActionManager.Instance.IsActionDoable(action, humanInfo)){
                GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
                ActionCell script = go.GetComponent<ActionCell>();
                script.InitCell(action, humanInfo, this);
            }
        }
        foreach (InventoryEnum inventory in HumanManager.Instance.heroInfo.inventories)
        {
            if (InventoryManager.Instance.IsInventoryUsable(inventory, humanInfo))
            {
                GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
                ActionCell script = go.GetComponent<ActionCell>();
                script.InitCell(inventory, humanInfo, this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

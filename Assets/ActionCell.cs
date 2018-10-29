using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ActionEnum { talk }

public class ActionCell : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Button actionButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitCell(ActionEnum action, HumanInfo humanInfo)
    {
        switch (action)
        {
            case ActionEnum.talk:
                description.text = "Talk";
                actionButton.onClick.AddListener(delegate {
                    DoAction();
                });
                break;
        }
    }

    void DoAction()
    {
        TurnBaseClock.Instance.UpdateTime();
    }

    public void InitCell(InventoryEnum inventory, HumanInfo humanInfo)
    {
        switch (inventory)
        {
            case InventoryEnum.stone:
                description.text = "Throw a stone";
                actionButton.onClick.AddListener(delegate {
                    DoAction();
                });
                break;
            case InventoryEnum.kitchenKnife:
                description.text = "Throw a kitchen knife";
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
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

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
    HumanStateViewController viewController;
    // Start is called before the first frame update
    void Start()
    {
        
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

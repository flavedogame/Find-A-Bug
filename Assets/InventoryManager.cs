using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryEnum { stone, kitchenKnife, shotgun, pistol, binoculars, megaphone, crossbow, handAxe, kevlarVest, grenade, potLid,  }
//harisen, flashlight, rope, dagger, sickle, fork, machete, switchBlade, 
public class InventoryManager : Singleton<InventoryManager>
{
    public List<InventoryEnum> inventories;
    // Start is called before the first frame update
    void Start()
    {
        inventories = new List<InventoryEnum>();
        inventories.Add(InventoryEnum.stone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

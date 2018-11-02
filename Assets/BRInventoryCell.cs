using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BRInventoryCell : MonoBehaviour
{
    public TextMeshProUGUI description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCell(InventoryEnum inventory)
    {
        
        description.text = InventoryManager.InventoryName(inventory);
    }

    public void InitCell(HumanInfo killer, HumanInfo killee)
    {
        description.text = killer.Name + " has killed " + killee.Name + ".";
    }

    public void InitCell(string message)
    {
        description.text = message;
    }

    public void InitCell(int hour)
    {
        description.text = "Red zone will be dead zone in " + hour + " hours.";
    }
}

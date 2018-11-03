using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRMessageViewController : Singleton<BRMessageViewController>
{
    public GameObject inventoryCell;
    public GameObject inventoryListPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddCell(HumanInfo killer, HumanInfo killee)
    {
            GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
            BRInventoryCell script = go.GetComponent<BRInventoryCell>();
            script.InitCell(killer,killee);
    }

    public void AddCell(int hour)
    {
        GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
        BRInventoryCell script = go.GetComponent<BRInventoryCell>();
        script.InitCell(hour);
    }

    public void AddBombCell()
    {
        AddCell("Bomb!");
    }

    public void AddCell(string message)
    {
        GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
        BRInventoryCell script = go.GetComponent<BRInventoryCell>();
        script.InitCell(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

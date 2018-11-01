using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRInventoryViewController : Singleton<BRInventoryViewController>
{
    public GameObject inventoryCell;
    public GameObject inventoryListPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInventoryView()
    {
        foreach (Transform child in inventoryListPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (InventoryEnum inventory in HumanManager.Instance.heroInfo.inventories)
        {

            GameObject go = Instantiate(inventoryCell, inventoryListPanel.transform);
            BRInventoryCell script = go.GetComponent<BRInventoryCell>();
            script.InitCell(inventory);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

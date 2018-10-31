﻿using System.Collections;
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
}
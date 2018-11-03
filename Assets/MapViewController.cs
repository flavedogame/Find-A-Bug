﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewController : Singleton<MapViewController>
{
    public List<MapCell> mapCells;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateMapColor(int id, Color color)
    {
        Debug.Log(id + " will change to color " + color);
        if (id >= 0 && id < 4)
        {
            mapCells[id].UpdateColor(color);
        }
        else
        {
            Debug.LogError(id + " is out of range");
        }
    }
        public void UpdateMap()
    {
        Vector3 heroPosition = HumanManager.Instance.heroInfo.transform.position;
        foreach (MapCell cell in mapCells)
        {
            cell.UpdateLocation(false);
        }
        if (heroPosition.x < 0 && heroPosition.y >= 0)
        {
            mapCells[0].UpdateLocation(true);
        }
        if (heroPosition.x>=0 && heroPosition.y >= 0)
        {
            mapCells[1].UpdateLocation(true);
        }
        if (heroPosition.x < 0 && heroPosition.y < 0)
        {
            mapCells[2].UpdateLocation(true);
        }
        if (heroPosition.x >= 0 && heroPosition.y < 0)
        {
            mapCells[3].UpdateLocation(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

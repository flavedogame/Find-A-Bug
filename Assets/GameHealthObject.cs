using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthObject : MonoBehaviour
{
    public int maxHP = 2;
    int currentHP;



    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

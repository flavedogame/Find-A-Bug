using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthObject : MonoBehaviour
{
    public int maxHP = 2;
    int currentHP;
    SimpleHealthBar healthBar;

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        healthBar.UpdateBar(currentHP);
    }

    void Start()
    {
        currentHP = maxHP;
        healthBar = GetComponentInChildren<SimpleHealthBar>();
        healthBar.SetupBar(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

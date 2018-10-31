using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarItem : FogOfWar
{
    public override void ClearFog()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
    }
    public override void UnclearFog()
    {
        sr.enabled = false;
    }

    public bool IsVisible()
    {
        return sr.enabled;
    }
    protected override void Start()
    {
       
    }
}

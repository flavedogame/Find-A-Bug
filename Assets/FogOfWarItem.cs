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
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public bool IsVisible()
    {
        sr = GetComponent<SpriteRenderer>();
        return sr.enabled;
    }
    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public GameObject fog;
    protected SpriteRenderer sr;
    public virtual void ClearFog()
    {
        sr = fog.GetComponent<SpriteRenderer>();
        fog.SetActive(false);
    }
    public virtual void UnclearFog()
    {
        fog.SetActive(true);

        sr.color = new Color(sr.color.r, sr.color.r, sr.color.r, 0.5f);
    }
    // Start is called before the first frame update
    protected virtual void  Start()
    {
        sr = fog.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

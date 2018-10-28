using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public GameObject fog;
    SpriteRenderer sr;
    public void ClearFog()
    {
        sr = fog.GetComponent<SpriteRenderer>();
        fog.SetActive(false);
    }
    public void UnclearFog()
    {
        fog.SetActive(true);

        sr.color = new Color(sr.color.r, sr.color.r, sr.color.r, 0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = fog.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAction : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DidTap()
    {
        if (sr.enabled)
        {
            HumanInfo info = GetComponent<HumanInfo>();
            HumanStateViewController.CreateViewController(info);
        }
    }
}

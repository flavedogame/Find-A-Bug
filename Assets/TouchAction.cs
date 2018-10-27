using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DidTap()
    {
        HumanInfo info = GetComponent<HumanInfo>();
        HumanStateViewController.CreateViewController(info);
    }
}

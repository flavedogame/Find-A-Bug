using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c : MonoBehaviour
{
    public float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        DestoryAfterAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestoryAfterAnimation()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}

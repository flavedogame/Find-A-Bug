using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupViewController : MonoBehaviour
{
    static public void CreateFoundItExplosionViewController()
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[5];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.foundItPopupCanvas.transform) as GameObject;
        Animator anim = go.GetComponent<Animator>();
        if (anim != null)
        {
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            float length = info.length;
            Debug.Log("Animation: " + length);
            Destroy(go, length);
        }
    }
}

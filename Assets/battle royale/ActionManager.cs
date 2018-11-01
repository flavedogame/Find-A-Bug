using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    int[] actionRange = new int[] { 5, 2 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsActionDoable(ActionEnum actionEnum, HumanInfo attackee)
    {
        HumanInfo heroInfo = HumanManager.Instance.heroInfo;
        if (attackee != heroInfo)
        {
            if (Vector3.Distance(attackee.transform.position, heroInfo.transform.position) <= actionRange[(int)actionEnum])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}

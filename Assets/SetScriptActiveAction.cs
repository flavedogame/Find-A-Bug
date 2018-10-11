using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetScriptActiveAction : NarrativeAction
{
    public SetScriptActiveAction(NarrativeInfo info) : base(info)
    {
        //Debug.Log("create tutorial action with info: " + narrativeInfo.identifier);
    }
    
    protected override void P_Enable()
    {
        string tagName = narrativeInfo.param["tag_name"];
        string scriptName = narrativeInfo.param["script_name"];
        MonoBehaviour mb = CoroutineMonobehavior.Instance.GetComponent<MonoBehaviour>();
        GameObject [] gos =  GameObject.FindGameObjectsWithTag(tagName);
        if (gos.Length == 0)
        {
            Debug.LogError(tagName + " not found in scene");
        }
        
        foreach(GameObject go in gos)
        {
            Behaviour script = go.GetComponent(scriptName) as Behaviour;
            if (script == null)
            {
                Debug.LogError(scriptName + " not found in object "+go +" with components "+ go.GetComponents(typeof(Behaviour)));
                foreach(Behaviour c in go.GetComponents(typeof(Behaviour)))
                {
                    Debug.Log("component " + c);
                    if(c.name.Equals( scriptName))
                    {
                        script = c;
                        Debug.Log("god knows why");
                        break;
                    }
                }
            }
            script.enabled = true;
        }
    }
}

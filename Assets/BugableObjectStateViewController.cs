using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BugableObjectStateViewController : DefaultViewController {
    public Image objectIcon;
    public TextMeshProUGUI objectDescription;
    public TextMeshProUGUI objectName;


    public BugableObjectStateViewController(BugableObjectInfo info)
    {
        //int index = 
        Object prefab = ViewControllerManager.Instance.viewControllers[0];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        BugableObjectStateViewController script = go.GetComponent<BugableObjectStateViewController>();
        script.Init(info);
    }

    public void Init(BugableObjectInfo info)
    {
        //Debug.Log("init BugableObjectStateViewController with info " + info.name);
        objectName.text = info.name;
        objectDescription.text = info.description;
        objectIcon.sprite = info.icon;
    }
}

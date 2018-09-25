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
        GameObject go = ViewControllerManager.Instance.viewControllers[0];
        Instantiate(go, ViewControllerManager.Instance.viewControllerCanvas.transform);
        BugableObjectStateViewController script = go.GetComponent<BugableObjectStateViewController>();
        script.Init(info);
    }

    public void Init(BugableObjectInfo info)
    {
        objectName.text = info.name;
        objectDescription.text = info.description;
        objectIcon.sprite = info.icon;
    }
}

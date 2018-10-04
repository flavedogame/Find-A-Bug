using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeveloperDialogViewController : DefaultViewController
{

    public TextMeshProUGUI dialogText;
    public Image dialogIcon;
    // Start is called before the first frame update

    static public void CreateViewController(List<NarrationInfo> narrationInfoList)
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[3];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        DeveloperDialogViewController script = go.GetComponent<DeveloperDialogViewController>();
        script.Init(narrationInfoList);
    }

    private void Init(List<NarrationInfo> narrationInfoList)
    {
        //DataService ds = SQLiteDatabaseManager.Instance.ds;
        //AddButton("clean object function", delegate { ds.DeleteAllObjectFunction(); BugableObjectFunctionManager.Instance.ReadDatabase(); });
    }
}

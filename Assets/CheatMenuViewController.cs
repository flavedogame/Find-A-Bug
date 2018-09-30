using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenuViewController : DefaultViewController
{
    public Object cheatButtonPrefab;
    public Transform listTransform;

    static public void CreateViewController()
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[2];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        CheatMenuViewController script = go.GetComponent<CheatMenuViewController>();
        script.Init();
    }

    private void Init()
    {
        GameObject go = Instantiate(cheatButtonPrefab, listTransform) as GameObject;
        Button button = go.GetComponent<Button>();
        Text name = go.GetComponentInChildren<Text>();
        name.text = "clean database";
        button.onClick.AddListener(delegate { Debug.Log("Hello"); });
    }
    
}

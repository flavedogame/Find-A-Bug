using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenuViewController : DefaultViewController
{
    public Object cheatButtonPrefab;
    public Transform listTransform;

    delegate void ButtonDelegate();

    static public void CreateViewController()
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[2];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        CheatMenuViewController script = go.GetComponent<CheatMenuViewController>();
        script.Init();
    }

    private void Init()
    {
        DataService ds = SQLiteDatabaseManager.Instance.ds;
        AddButton("clean object function", delegate {  ds.DeleteAllObjectFunction(); BugableObjectFunctionManager.Instance.ReadDatabase(); });
        AddButton("clean Achievement", delegate
        {
            AchievementManager.Instance.CleanAchievements();
        }
            );

    }
    void AddButton(string name,ButtonDelegate dele) 
    {
        GameObject go = Instantiate(cheatButtonPrefab, listTransform) as GameObject;
        Button button = go.GetComponent<Button>();
        Text text = go.GetComponentInChildren<Text>();
        text.text = name;
        button.onClick.AddListener(delegate { dele(); });
    }
    
}

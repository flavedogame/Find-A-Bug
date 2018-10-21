using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoListViewController : DefaultViewController
{
    public GameObject todoListCell;
    public GameObject todoListListPanel;
    // Use this for initialization

    static public void CreateViewController()
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[6];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        TodoListViewController script = go.GetComponent<TodoListViewController>();
        script.Init();
    }

    private void Start()
    {
        Init();

        AchievementManager.Instance.RegisterAchievementComplete(Init);
    }

    void Init()
    {
        //todo: don't destroy them, reuse them
        foreach (Transform child in todoListListPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (string todoListKey in TodoListManager.Instance.activeTodoListList)
        {
            foreach (TodoListInfo info in TodoListManager.Instance.activeTodoListDictionary[todoListKey])
            {
                GameObject go = Instantiate(todoListCell, todoListListPanel.transform);
                TodoListCell script = go.GetComponent<TodoListCell>();
                script.InitCell(info);
            }
            
        }
    }
}

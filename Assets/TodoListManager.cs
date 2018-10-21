using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class TodoListManager : Singleton<TodoListManager>
{
    public Dictionary<string, TodoListInfo> todoListInfoDictionary { get; private set; }
    public List<string> activeTodoListList { get; private set; }
    public Dictionary<string, List<TodoListInfo>> activeTodoListDictionary { get; private set; }// does not support more than 1 level of hiarchy
    public List<TodoListInfo> todoListList{ get; private set; }
public void Init()
    {
        InitCSV();
        InitList();
    }

    void InitCSV()
    {
        todoListInfoDictionary = new Dictionary<string, TodoListInfo>();
        todoListList = CsvUtil.LoadObjects<TodoListInfo>("todoList.csv");
        foreach (TodoListInfo todoListInfo in todoListList)
        {
            todoListInfoDictionary[todoListInfo.identifier] = todoListInfo;
        }
    }
    public void InitList()
    {
        activeTodoListList = new List<string>();
        activeTodoListDictionary = new Dictionary<string, List<TodoListInfo>>();
        foreach (TodoListInfo todoListInfo in todoListList)
        {
            //is child list
            if (todoListInfo.parentList != "")
            {
                //parent already added
                if (activeTodoListDictionary.ContainsKey(todoListInfo.parentList))
                {
                    activeTodoListDictionary[todoListInfo.parentList].Add(todoListInfo);
                }
                else
                {
                    //Debug.LogError("parent "+ todoListInfo.parentList+" does not exist in todo list: " + todoListInfo);
                    continue;
                }
            }
            //is parent list
            else
            {
                if (AchievementManager.Instance.achievementDictionary[ todoListInfo.startAchievement].state == AchievementState.active &&
                    AchievementManager.Instance.achievementDictionary[todoListInfo.endAchievement].state != AchievementState.complete)
                {
                    activeTodoListDictionary[todoListInfo.identifier] = new List<TodoListInfo>();
                    activeTodoListDictionary[todoListInfo.identifier].Add(todoListInfo);
                    //sort active todo list
                    activeTodoListList.Add(todoListInfo.identifier);
                }
            }

        }
    }
}

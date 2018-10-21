using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TodoListCell : MonoBehaviour
{
    public GameObject checkIcon;
    public TextMeshProUGUI description;
    // Use this for initialization
    void Start()
    {

    }

    public void InitCell(TodoListInfo todoListInfo)
    {
        description.text = todoListInfo.description;
        if (AchievementManager.Instance.achievementDictionary[todoListInfo.finishAchievement].state == AchievementState.complete)
        {
            checkIcon.SetActive(true);
        }
        else
        {
            checkIcon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

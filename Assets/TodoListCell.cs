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
            description.color = new Color(0, 1, 0);
        }
        else
        {

            checkIcon.SetActive(false);
            description.color = new Color(1, 1, 0);
        }
        if (todoListInfo.parentList.Length !=0)
        {
            //todo: does not support multi level nesting
            description.rectTransform.offsetMin = new Vector2(40, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

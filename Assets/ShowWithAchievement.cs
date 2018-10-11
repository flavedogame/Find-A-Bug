using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWithAchievement : MonoBehaviour
{
    public string achievementIdentifier;
    AchievementCompleteDelegate dele;
    // Start is called before the first frame update
    void Start()
    {
        if (AchievementManager.Instance.achievementDictionary[achievementIdentifier].state != AchievementState.complete)
        {
            gameObject.SetActive(false);
            dele = delegate {
                if(AchievementManager.Instance.achievementDictionary[achievementIdentifier].state == AchievementState.complete)
                {

                    gameObject.SetActive(true);
                    AchievementManager.Instance.RemoveAchievementComplete(dele);
                }
            };
            AchievementManager.Instance.RegisterAchievementComplete(dele);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

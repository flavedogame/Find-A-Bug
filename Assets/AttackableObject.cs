using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableObject : MonoBehaviour
{
    public int attack;
    public int rangeStart;
    public int rangeEnd;
    public int hp;
    public bool isControlledByPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isControlledByPlayer && 
            AchievementManager.Instance.achievementDictionary["finishWallBugNarration"].state == AchievementState.complete)
        {
            AchievementManager.Instance.FinishAchievement("firstTimeAttackMonster");

            //temp
            NarrationManager.Instance.ShowNarrationWithIdentifier("v1Dialog");
        }
    }

    void GetDamage()
    {

    }
}

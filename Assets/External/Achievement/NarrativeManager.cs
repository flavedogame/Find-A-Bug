using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class NarrativeManager : Singleton<NarrativeManager> {
    Dictionary<string, HashSet<NarrativeAction>> narrativeActionDictionary;
    Dictionary<string, NarrativeInfo> narrativeInfoDictionary;
    List<NarrativeAction> activeNarrativeActions;
    HashSet<string> enabledNarrativeActions;
    public void Init() {
        List<NarrativeInfo> narrativeInfoList = CsvUtil.LoadObjects<NarrativeInfo>("narrative.csv");
        narrativeActionDictionary = new Dictionary<string, HashSet<NarrativeAction>>();
        narrativeInfoDictionary = new Dictionary<string, NarrativeInfo>();
        enabledNarrativeActions = new HashSet<string>();
        activeNarrativeActions = new List<NarrativeAction>();
        foreach(NarrativeInfo info in narrativeInfoList)
        {
            narrativeInfoDictionary[info.identifier] = info;
            System.Type narrativeType = System.Type.GetType(info.narrativeAction);
            NarrativeAction action = (NarrativeAction)System.Activator.CreateInstance(narrativeType, info);
            Achievement achievement = AchievementManager.Instance.achievementDictionary[info.achievement];
            if (achievement.state == AchievementState.complete)
            {
                continue;
            }
            if (achievement.state == AchievementState.active)
            {
                activeNarrativeActions.Add(action);
            }
                if (!narrativeActionDictionary.ContainsKey(info.achievement))
                {
                    narrativeActionDictionary[info.achievement] = new HashSet<NarrativeAction>();
                }
                narrativeActionDictionary[info.achievement].Add(action);
            
        }
    }

    public void UpdateAchievement(string achievement, AchievementState oldState,AchievementState newState)
    {
        if (narrativeActionDictionary == null)
        {
            return;
        }
        if(oldState == AchievementState.locked && newState == AchievementState.active)
        {

            Debug.Log("update " + achievement + " " + oldState + " " + newState);
            if (narrativeActionDictionary.ContainsKey(achievement)) { 
                activeNarrativeActions.AddRange(narrativeActionDictionary[achievement]);
            }
        }
        //sort
    }

    private void Update()
    {
        if (narrativeActionDictionary == null)
        {
            return;
        }
        foreach (NarrativeAction action in activeNarrativeActions)
        {
            if (!enabledNarrativeActions.Contains(action.identifier))
            {
                action.Enable();
                enabledNarrativeActions.Add(action.identifier);
            }
        }
    }

    public void FinishNarrative(string identifier)
    {
        NarrativeInfo narrativeInfo = narrativeInfoDictionary[identifier];

        Debug.Log("finish narrative " + identifier+" "+ narrativeInfo+" "+ narrativeInfo.achievement);
        if (narrativeInfo.achievement!=null && narrativeInfo.achievement.Length > 0)
        {
            AchievementManager.Instance.FinishAchievement(narrativeInfo.achievement);
        }
    }
}

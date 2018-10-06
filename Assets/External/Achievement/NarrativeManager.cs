using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class NarrativeManager : Singleton<NarrativeManager> {
    Dictionary<string, HashSet<NarrativeAction>> narrativeActionDictionary;
    Dictionary<string, NarrativeInfo> narrativeInfoDictionary;
    List<NarrativeAction> activeNarrativeActions;
    HashSet<string> enabledNarrativeActionNames;
    HashSet<NarrativeAction> enabledNarrativeActions;
    public void Init() {
        List<NarrativeInfo> narrativeInfoList = CsvUtil.LoadObjects<NarrativeInfo>("narrative.csv");
        narrativeActionDictionary = new Dictionary<string, HashSet<NarrativeAction>>();
        narrativeInfoDictionary = new Dictionary<string, NarrativeInfo>();
        enabledNarrativeActionNames = new HashSet<string>();
        enabledNarrativeActions = new HashSet<NarrativeAction>();
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

    public void SetupNarrativeInfoList()
    {

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
            if (!enabledNarrativeActionNames.Contains(action.identifier))
            {
                action.Enable();
                enabledNarrativeActionNames.Add(action.identifier);
            }
        }
    }

    public void FinishNarrative(string identifier)
    {
        NarrativeInfo narrativeInfo = narrativeInfoDictionary[identifier];

        Debug.Log("finish narrative " + identifier+" "+ narrativeInfo+" "+ narrativeInfo.finishAchievement);
        if (narrativeInfo.finishAchievement != null && narrativeInfo.finishAchievement.Length > 0)
        {
            AchievementManager.Instance.FinishAchievement(narrativeInfo.finishAchievement);
        }
    }
}

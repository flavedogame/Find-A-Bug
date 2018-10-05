using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class NarrationManager : Singleton<NarrationManager> {
    
    Dictionary<string, List<NarrationInfo>> narrationDictionary;
    public void Init() {
        List<NarrationInfo> narrationInfoList = CsvUtil.LoadObjects<NarrationInfo>("narration.csv");
        narrationDictionary = new Dictionary<string, List<NarrationInfo>>();
        foreach(NarrationInfo info in narrationInfoList)
        {
            if (!info.isVisibleForTest && CheatSettings.Instance.skipTestingNarrations)
            {
                continue;
            }
            string identifier = info.identifier;
            string[] splitIdentifier = info.identifier.Split('_');
            if (splitIdentifier.Length > 2)
            {
                Debug.LogError("narration identifier format is wrong " + info.identifier);
            }
            if(splitIdentifier.Length == 2)
            {
                identifier = splitIdentifier[0];
            }
            if (!narrationDictionary.ContainsKey(identifier))
            {
                narrationDictionary[identifier] = new List<NarrationInfo>();
            }
            narrationDictionary[identifier].Add(info);
        }
    }

    public bool IsShowingNarrationWithIdentifier(string identifier)
    {
        return false;
    }

    public void ShowNarrationWithIdentifier(NarrativeInfo narrativeInfo)//delegate, tag, give choice
    {
        string identifier = narrativeInfo.NarrationId;
        string narrativeIdentifier = narrativeInfo.identifier;
        Debug.Log("show narration " + identifier);
        if (!narrationDictionary.ContainsKey(identifier))
        {
            Debug.LogError("identifier does not exist in narration dict " + identifier);
        }
        List<NarrationInfo> narrationInfos = narrationDictionary[identifier];
        DeveloperDialogViewController.CreateViewController(narrativeIdentifier,narrationInfos);
    }
}

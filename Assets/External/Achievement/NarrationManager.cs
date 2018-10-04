using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class NarrationManager : Singleton<NarrationManager> {
    Dictionary<string, List<NarrationInfo>> narrationDictionary;
    List<NarrativeAction> activeNarrativeActions;
    HashSet<string> enabledNarrativeActions;
    public void Init() {
        List<NarrationInfo> narrationInfoList = CsvUtil.LoadObjects<NarrationInfo>("narration.csv");
        narrationDictionary = new Dictionary<string, List<NarrationInfo>>();
        foreach(NarrationInfo info in narrationInfoList)
        {
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

    public void ShowNarrationWithIdentifier(string identifier)//delegate, tag, give choice
    {
        Debug.Log("show narration " + identifier);
        if (!narrationDictionary.ContainsKey(identifier))
        {
            Debug.LogError("identifier does not exist in narration dict " + identifier);
        }
        List<NarrationInfo> narrationInfos = narrationDictionary[identifier];
    }
}

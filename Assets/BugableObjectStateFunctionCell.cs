using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BugableObjectStateFunctionCell : MonoBehaviour {

    public TextMeshProUGUI functionDescription;

    public BugableObjectStateFunctionCell(BugableObjectFunctionInfo info, Object cellPrefab, Transform tableTransform)
    {
        GameObject go = Instantiate(cellPrefab, tableTransform) as GameObject;
        BugableObjectStateFunctionCell script = go.GetComponent<BugableObjectStateFunctionCell>();
        script.Init(info);
    }

    public void Init(BugableObjectFunctionInfo info)
    {
        functionDescription.text = info.description;
    }
}

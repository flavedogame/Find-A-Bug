using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BugableObjectStateFunctionCell : MonoBehaviour {

    public TextMeshProUGUI functionDescription;
    public GameObject newBanner;
    Button cellBackground;
    BugableObjectFunctionInfo functionInfo;


    public BugableObjectStateFunctionCell(BugableObjectFunctionInfo info, Object cellPrefab, Transform tableTransform)
    {
        GameObject go = Instantiate(cellPrefab, tableTransform) as GameObject;
        BugableObjectStateFunctionCell script = go.GetComponent<BugableObjectStateFunctionCell>();
        script.Init(info);
    }

    public void Init(BugableObjectFunctionInfo info)
    {
        functionInfo = info;
        UpdateView();
    }

    public void UpdateView()
    {
        functionDescription.text = functionInfo.description;
        newBanner.SetActive(!functionInfo.isViewed);
        cellBackground = GetComponent<Button>();
        cellBackground.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (!functionInfo.isViewed)
        {
            CurrencyManager.Instance.AddValue("points", 1);
            BugableObjectFunctionManager.Instance.ViewFunction(functionInfo);
            UpdateView();
        } else
        {
            Debug.Log("already collected for function " + functionInfo.identifier);
        }
    }
}

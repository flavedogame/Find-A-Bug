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
    public GameObject newParticleEffect;
    public GameObject tapParticleEffect;


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
        newParticleEffect.SetActive(false);
        tapParticleEffect.SetActive(false);
    }

    public void UpdateView()
    {
        functionDescription.text = functionInfo.description;
        bool isNew = !functionInfo.isViewed;
        newBanner.SetActive(isNew);
        newParticleEffect.SetActive(isNew);
        cellBackground = GetComponent<Button>();
        cellBackground.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (!functionInfo.isViewed)
        {
            CurrencyManager.Instance.AddValue("points", 1);
            BugableObjectFunctionManager.Instance.ViewFunction(functionInfo);
            SFXManager.Instance.PlaySFX(SFXEnum.getPoint);
            UpdateView();
            tapParticleEffect.SetActive(true);
        } else
        {
            Debug.Log("already collected for function " + functionInfo.identifier);
        }
    }
}

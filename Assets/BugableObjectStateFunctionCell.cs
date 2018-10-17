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

    public Image newOverlayRender;
    public Image tapOverlayRender;
    Color newTransparentColor;
    Color newPulseColor;
    Color tapTransparentColor;
    Color tapPulseColor;

    bool shouldNewOverlayShow;
    

    public BugableObjectStateFunctionCell(BugableObjectFunctionInfo info, Object cellPrefab, Transform tableTransform)
    {
        GameObject go = Instantiate(cellPrefab, tableTransform) as GameObject;
        BugableObjectStateFunctionCell script = go.GetComponent<BugableObjectStateFunctionCell>();
        script.Init(info);
    }

    public void Init(BugableObjectFunctionInfo info)
    {
        functionInfo = info;
        newPulseColor = newOverlayRender.color;
        newTransparentColor = new Color(newPulseColor.r, newPulseColor.g, newPulseColor.b, 0);
        tapPulseColor = tapOverlayRender.color;
        tapTransparentColor = new Color(tapPulseColor.r, tapPulseColor.g, tapPulseColor.b, 0);
        newOverlayRender.gameObject.SetActive(false);
        tapOverlayRender.gameObject.SetActive(false);
        UpdateView();
    }

    public void UpdateView()
    {
        functionDescription.text = functionInfo.description;
        bool isNew = !functionInfo.isViewed;
        newBanner.SetActive(isNew);
        shouldNewOverlayShow = isNew;
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

            tapOverlayRender.gameObject.SetActive(true);
            Animator anim = tapOverlayRender.GetComponent<Animator>();
            anim.Rebind();
            if (anim != null)
            {
                AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
                float length = info.length;
                Destroy(tapOverlayRender, length);
            }
        } else
        {
            Debug.Log("already collected for function " + functionInfo.identifier);
        }
    }

    void Update()
    {

        if (shouldNewOverlayShow)
        {
            newOverlayRender.gameObject.SetActive(true);
            newOverlayRender.color = Color.Lerp(newTransparentColor, newPulseColor, Mathf.PingPong(Time.time, 0.5f));
        }
        else
        {
            newOverlayRender.gameObject.SetActive(false);
        }
    }
}

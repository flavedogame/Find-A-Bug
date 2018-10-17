using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeveloperDialogViewController : DefaultViewController
{

    public TextMeshProUGUI dialogText;
    public Image dialogIcon;
    List<NarrationInfo> narrationInfoList;
    int currentNarrationFrameIndex;
    string narrativeIdentifier;
    TeleType teleType;
    // Start is called before the first frame update

    static public void CreateViewController(string identifier, List<NarrationInfo> narrationInfoList)
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[3];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        DeveloperDialogViewController script = go.GetComponent<DeveloperDialogViewController>();
        script.Init(identifier,narrationInfoList);
    }

    private void Init(string identifier, List<NarrationInfo> list)
    {
        narrativeIdentifier = identifier;
        narrationInfoList = list;
        currentNarrationFrameIndex = -1;
        teleType = dialogText.GetComponent<TeleType>();
        SetupView();
        ShowNextFrame();
    }

    bool IsLastFrame()
    {
        return currentNarrationFrameIndex == narrationInfoList.Count-1;
    }

    void OnClick()
    {
        if (IsLastFrame())
        {
            if (narrativeIdentifier != null)
            {
                NarrativeManager.Instance.FinishNarrative(narrativeIdentifier);
            }
            Destroy(gameObject);
        }
        else
        {
            ShowNextFrame();
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    void SetupView()
    {

    }

    void ShowNextFrame()
    {
        currentNarrationFrameIndex++;
        NarrationInfo currentNarrationInfo = CurrentNarrationInfo();
        if (currentNarrationInfo != null)
        {
            UpdateView(currentNarrationInfo);
        } else
        {
            Debug.LogError("narration info could not be found for index " + currentNarrationFrameIndex);
        }
    }

    void UpdateView(NarrationInfo narrationInfo)
    {
        dialogText.text = narrationInfo.dialog;
        teleType.Init();
    }

    NarrationInfo CurrentNarrationInfo()
    {
        if (currentNarrationFrameIndex < narrationInfoList.Count)
        {
            return narrationInfoList[currentNarrationFrameIndex];
        }
        return null;
    }
}

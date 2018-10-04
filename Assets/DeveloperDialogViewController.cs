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
    // Start is called before the first frame update

    static public void CreateViewController(List<NarrationInfo> narrationInfoList)
    {

        Object prefab = ViewControllerManager.Instance.viewControllers[3];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        DeveloperDialogViewController script = go.GetComponent<DeveloperDialogViewController>();
        script.Init(narrationInfoList);
    }

    private void Init(List<NarrationInfo> list)
    {
        narrationInfoList = list;
        currentNarrationFrameIndex = -1;
        SetupView();
        ShowNextFrame();
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

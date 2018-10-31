using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : DefaultViewController
{
    public TextMeshProUGUI dialog;
    List<string> dialogs;
    public Button button;

    int currentDialogIndex = 0;

    static public void CreateViewController(List<string> strings)
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[8];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        DialogManager script = go.GetComponent<DialogManager>();
        script.Init(strings);
    }

    void Init(List<string> strings)
    {

        dialogs = strings;
        UpdateDialog();
        button.onClick.AddListener(delegate {
            DidClick();
        });
    }

    void UpdateDialog()
    {
        dialog.text = dialogs[currentDialogIndex];
    }

    void DidClick()
    {
        currentDialogIndex++;
        if (currentDialogIndex >= dialogs.Count)
        {
            Back();
        }
        else
        {
            UpdateDialog();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public delegate void DialogEndDelegate();
public class DialogManager : DefaultViewController
{
    public TextMeshProUGUI dialog;
    List<string> dialogs;
    public Button button;
    DialogEndDelegate dialogEndDele;

    int currentDialogIndex = 0;

    static public void CreateViewController(List<string> strings, DialogEndDelegate dialogEndDelegate)
    {
        Object prefab = ViewControllerManager.Instance.viewControllers[8];
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        DialogManager script = go.GetComponent<DialogManager>();
        script.Init(strings, dialogEndDelegate);
        BRDialogManager.Instance.AddDialog();
    }

    static public void CreateViewController(List<string> strings)
    {
        CreateViewController(strings, null);
    }

    static public void CreateViewController(string strings)
    {
        List<string> l = new List<string>();
        l.Add(strings);
        CreateViewController(l);
    }

    void Init(List<string> strings, DialogEndDelegate dialogEndDelegate)
    {
        dialogEndDele = dialogEndDelegate;
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
            dialogEndDele?.Invoke();
            Back();
            BRDialogManager.Instance.CloseDialog();
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

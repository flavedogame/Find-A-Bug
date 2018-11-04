using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDialogManager : Singleton<BRDialogManager>
{
    int dialogNum;
    public void AddDialog()
    {
        dialogNum++;
    }
    public void CloseDialog()
    {
        dialogNum--;
    }
    public bool IsNoDialog()
    {
        return dialogNum == 0;
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

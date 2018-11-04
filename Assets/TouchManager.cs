using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{

    public Texture2D cursorTexture;
    public Texture2D cursorTextureHit;

    enum CursorStateEnum { playMode, findBugNormal, findBugHit };
    CursorStateEnum cursorState;

    // Update is called once per frame
    void Update()
    {
        if (!BRDialogManager.Instance.IsNoDialog())
        {
            return;
        }
        //if (GameLogicManager.Instance.isPaused)
        //{
        //    return;
        //}
        //#if !UNITY_EDITORs
        //if (Input.touchCount == 1)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase != TouchPhase.Began)
        //    {
        //        return;
        //    }
        //    //Your raycast handling
        //    Vector3 mousePosition = touch.position;
        //    //CSUtil.LOG("screen touched " + mousePosition);
        //    mousePosition = new Vector3(mousePosition.x, mousePosition.y, 10);
        //    RaycastHit2D[] vHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
        //    bool getHit = false;
        //    foreach (RaycastHit2D vhit in vHits)
        //    {
        //        if (vhit.transform.tag == "BugableObject")
        //        {
        //            BugableObject script = vhit.transform.gameObject.GetComponent<BugableObject>();
        //            script.DidTap();
        //            getHit = true;
        //        }
        //    }
        //    if(getHit == false)
        //    {
        //        //should not do this here, but we should show different cursor when it is on something that is a bug object to when it is not
        //    }
        //}
        //#endif


        // Your raycast handling
        Vector3 mousePosition = Input.mousePosition;
        //CSUtil.LOG("mouse click " + mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 10);
        RaycastHit2D[] vHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
        bool hasHitted = false;
        foreach (RaycastHit2D vhit in vHits)
        {
            if (vhit.transform.tag == "BugableObject" && vhit.collider.isTrigger)
            {

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    TouchAction script = vhit.transform.gameObject.GetComponent<TouchAction>();
                    FogOfWarItem fowItem = vhit.transform.gameObject.GetComponent<FogOfWarItem>();
                    if (fowItem.IsVisible())
                    {
                        script.DidTap();
                    }
                }
                //improve this later, don't call manager in update
                if (GameModeManager.Instance.isInFindBugMode)
                {
                    ChangeCursorState(CursorStateEnum.findBugHit);
                    hasHitted = true;
                }
            }
        }
        if (!GameModeManager.Instance.isInFindBugMode)
        {
            ChangeCursorState(CursorStateEnum.playMode);
        }
        else if (!hasHitted)
        {
            ChangeCursorState(CursorStateEnum.findBugNormal);
        }

    }

    void ChangeCursorState(CursorStateEnum newState)
    {
        if (cursorState != newState)
        {
            switch (newState)
            {
                case CursorStateEnum.findBugNormal:
                    Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorStateEnum.findBugHit:
                    Cursor.SetCursor(cursorTextureHit, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorStateEnum.playMode:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
            cursorState = newState;
        }
    }
}

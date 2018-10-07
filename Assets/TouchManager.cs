﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {


    // Update is called once per frame
    void Update()
    {
        //if (GameLogicManager.Instance.isPaused)
        //{
        //    return;
        //}
        //#if !UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
            {
                return;
            }
            //Your raycast handling
            Vector3 mousePosition = touch.position;
            //CSUtil.LOG("screen touched " + mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 10);
            RaycastHit2D[] vHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
            bool getHit = false;
            foreach (RaycastHit2D vhit in vHits)
            {
                if (vhit.transform.tag == "BugableObject")
                {
                    BugableObject script = vhit.transform.gameObject.GetComponent<BugableObject>();
                    script.DidTap();
                    getHit = true;
                }
            }
            if(getHit == false)
            {
                //should not do this here, but we should show different cursor when it is on something that is a bug object to when it is not
            }
        }
        //#endif

        if (Input.GetMouseButtonDown(0))
        {
            // Your raycast handling
            Vector3 mousePosition = Input.mousePosition;
            //CSUtil.LOG("mouse click " + mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 10);
            RaycastHit2D[] vHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
            foreach (RaycastHit2D vhit in vHits)
            {
                if (vhit.transform.tag == "BugableObject" && vhit.collider.isTrigger)
                {
                    BugableObject script = vhit.transform.gameObject.GetComponent<BugableObject>();
                    script.DidTap();
                }
            }
        }
    }
}

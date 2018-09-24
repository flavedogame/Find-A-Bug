using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewControllerEnum { BugableObjectStateViewController };

public class ViewControllerManager : Singleton<ViewControllerManager>
{
    public List<GameObject> viewControllers;
    public GameObject viewControllerCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

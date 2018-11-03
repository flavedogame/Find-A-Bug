using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    public Image panelImage;
    public GameObject locationMark;
    public void UpdateLocation(bool isLocate)
    {
            locationMark.SetActive(isLocate);
    }

    public void UpdateColor(Color color)
    {
        panelImage.color = color;
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

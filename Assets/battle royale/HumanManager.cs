using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : Singleton<HumanManager>
{
   public  Dictionary<string,GameObject> HumanDict;
    public List<GameObject> AliveHuman;
    public GameObject hero;
    public HumanInfo heroInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

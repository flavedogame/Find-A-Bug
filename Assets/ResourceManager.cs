using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BugableObjectIconEnum { player,wall,monster}

public class ResourceManager : Singleton<ResourceManager>
{
    public TextMeshProUGUI leftPoepleText;
    int leftPeople;
    public int LeftPeople { get { return leftPeople; }
        set { leftPeople = value;
            leftPoepleText.text = ""+(OtherHumanManager.Instance.aliveHumans.Count+ (HumanManager.Instance.heroInfo.IsAlive?1:0))+" students left"; } }
    public List<Sprite> bugableObjectIcons;
    public List<Sprite> girlImages; public List<Sprite> boyImages;

}

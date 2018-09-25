using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BugableObjectIconEnum { player,wall}

public class ResourceManager : Singleton<ResourceManager>
{
    public List<Sprite> bugableObjectIcons;
	
}

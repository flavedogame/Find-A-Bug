using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BRHintEnum { gun,step};
public class BRHintManager : Singleton<BRHintManager>
{
    public GameObject gunPrefab;
    public GameObject stepPrefab;
    public Transform hintsTransform;
    List<Vector3> gunPositions;
    List<Vector3> stepPositions;

    bool hearedGun;
    bool hearedStep;
    public void AddHint(Vector3 position, BRHintEnum hintEnum){
        Vector3 heroPosition = HumanManager.Instance.hero.transform.position;
        float range = HumanManager.Instance.heroInfo.SightRange() ;
        switch (hintEnum)
        {
            case BRHintEnum.gun:
                gunPositions.Add(position);
                break;
            case BRHintEnum.step:
                float distance = Vector3.Distance(heroPosition, position);
                if (distance < 10 && distance > range)
                {
                    stepPositions.Add(position);
                }
                break;
        }
    }

    public void ShowHints()
    {
        Vector3 heroPosition = HumanManager.Instance.hero.transform.position;
        float range = HumanManager.Instance.heroInfo.SightRange()+1.3f;
        //show gun
        if (gunPositions.Count!=0)
        {
            if (!hearedGun)
            {
                hearedGun = true;
                DialogManager.CreateViewController("You heared gun shots nearby");
            }
            foreach(Vector3 position in gunPositions)
            {
                Vector3 dir = (position - heroPosition).normalized;
                GameObject go = Instantiate(gunPrefab, heroPosition + dir* range, Quaternion.identity, hintsTransform);
            }
            SFXManager.Instance.PlaySFX(SFXEnum.shotgun);
        }
        else if (stepPositions.Count != 0)
        {
            if (!hearedStep)
            {
                hearedStep = true;
                DialogManager.CreateViewController("You heared steps nearby");
            }
            foreach (Vector3 position in stepPositions)
            {
                Vector3 dir = (position - heroPosition).normalized;
                GameObject go = Instantiate(stepPrefab, heroPosition + dir * range, Quaternion.identity, hintsTransform);
            }
            SFXManager.Instance.PlaySFX(SFXEnum.grassStep);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPositions();
    }

    public void SetPositions()
    {
        gunPositions = new List<Vector3>();
        stepPositions = new List<Vector3>();
        foreach (Transform child in hintsTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherHumanManager : Singleton<OtherHumanManager>
{
    public List<HumanInfo> otherHumans;
    public List<HumanInfo> aliveHumans;

    public void AddHumanInfo(HumanInfo humanInfo)
    {
        if (otherHumans == null)
        {
            otherHumans = new List<HumanInfo>();
            aliveHumans = new List<HumanInfo>();
        }
        otherHumans.Add(humanInfo);
        aliveHumans.Add(humanInfo);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OtherHuamnMove()
    {
        foreach(HumanInfo humanInfo in otherHumans)
        {
            if(humanInfo.healthDescriptionEnum == HealthDescriptionEnum.dead)
            {
                aliveHumans.Remove(humanInfo);
                BoxCollider2D[] colliders = humanInfo.GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D collider in colliders)
                {
                    if (collider.isTrigger == false)
                    {
                        collider.enabled = false;
                    }
                }
                continue;
            }
            int sightRange = humanInfo.SightRange();
            if (humanInfo.targetHumanInfo &&
                Vector3.Distance(humanInfo.transform.position, humanInfo.targetHumanInfo.transform.position) <= sightRange &&
                humanInfo.targetHumanInfo.IsAlive)
            {
                //can still attack last target, why not
                humanInfo.Attack(humanInfo.targetHumanInfo);
                Debug.Log(humanInfo.Name + " attack target " + humanInfo.targetHumanInfo.Name);
                continue;
            } else
            {
                humanInfo.targetHumanInfo = null;
            }
            humanInfo.gameObject.layer = LayerMask.NameToLayer("Default");
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(humanInfo.transform.position, sightRange, 1 << LayerMask.NameToLayer("Player"));
            humanInfo.gameObject.layer = LayerMask.NameToLayer("Player");
            if (hitColliders.Length > 0)//there is at least one target
            {
                List<HumanInfo> validHumanInfos = new List<HumanInfo>();
                foreach(Collider2D col in hitColliders)
                {
                    HumanInfo script = col.GetComponent<HumanInfo>();
                    if (script && script.IsAlive)
                    {
                        validHumanInfos.Add(script);
                    }
                }
                if (validHumanInfos.Count > 0)
                {
                    //randomly pick one for now
                    HumanInfo colScript = validHumanInfos[Random.Range(0, validHumanInfos.Count)];
                    if (colScript)
                    {
                        humanInfo.Attack(colScript);

                        humanInfo.remainTargetTransformMovement = 0;
                        continue;
                    }
                }
                
            }
            {
                //let's move
                if (humanInfo.remainTargetTransformMovement > 0 && 
                    Vector3.Distance(humanInfo.transform.position,humanInfo.targetTransform.position)>=1.5f)
                {
                    //already knows where to move
                    float offsetX = Mathf.Abs(humanInfo.transform.position.x - humanInfo.targetTransform.position.x);
                    float offsetY = Mathf.Abs(humanInfo.transform.position.y - humanInfo.targetTransform.position.y);
                    if (offsetX >= offsetY)
                    {
                        if (humanInfo.targetTransform.position.x - humanInfo.transform.position.x > 0)
                        {
                            humanInfo.transform.position += new Vector3(1, 0, 0);
                        }
                        else
                        {
                            humanInfo.transform.position += new Vector3(-1, 0, 0);
                        }
                    }
                    else
                    {
                        if (humanInfo.targetTransform.position.y - humanInfo.transform.position.y > 0)
                        {
                            humanInfo.transform.position += new Vector3(0, 1, 0);
                        }
                        else
                        {
                            humanInfo.transform.position += new Vector3(0, -1, 0);
                        }
                    }

                    humanInfo.remainTargetTransformMovement -= 1;
                    BRHintManager.Instance.AddHint(humanInfo.transform.position, BRHintEnum.step);
                } else
                {
                    //either move to player or move to a random player
                    int chanceMoveToHero = Random.Range(0, 100);
                    if (chanceMoveToHero > 80)
                    {
                        humanInfo.targetTransform = HumanManager.Instance.heroInfo.transform;
                    } else
                    {
                        HumanInfo randomInfo = aliveHumans[Random.Range(0, aliveHumans.Count)];
                        humanInfo.targetTransform = randomInfo.transform;
                    }

                    humanInfo.remainTargetTransformMovement = 10;
                }
            }

            if (DeadZoneManager.Instance.IsInBombedDeadZone(humanInfo.transform.position))
            {
                humanInfo.HurtHuman(1000, "DEAD ZONE");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

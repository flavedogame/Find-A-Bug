using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherHumanManager : Singleton<OtherHumanManager>
{
    public List<HumanInfo> otherHumans;
    public List<HumanInfo> aliveHumans;
    int[] AttackChanceWithRelationship = new int[] { 10, 10, 20, 20, 30, 40, 55, 60 };

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
        List<string> dialogs = new List<string>();
        HumanInfo heroInfo = HumanManager.Instance.heroInfo;
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
                bool giveup = Random.Range(0, 100)>50;
                if (!giveup)
                {

                    humanInfo.Attack(humanInfo.targetHumanInfo);
                    Debug.Log(humanInfo.Name + " attack target " + humanInfo.targetHumanInfo.Name);
                    continue;
                } else
                {
                    humanInfo.targetHumanInfo = null;
                }
            } else
            {
                humanInfo.targetHumanInfo = null;
            }
            humanInfo.gameObject.layer = LayerMask.NameToLayer("Default");
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(humanInfo.transform.position, sightRange-1, 1 << LayerMask.NameToLayer("Player"));
            humanInfo.gameObject.layer = LayerMask.NameToLayer("Player");

            float attackChanceRatio = (HumanManager.Instance.numberOfHuman -  HumanManager.Instance.AliveHuman.Count);
            attackChanceRatio = attackChanceRatio / (float)HumanManager.Instance.numberOfHuman;
            attackChanceRatio += 1;

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
                bool acted = false;
                foreach(HumanInfo validHumanInfo in validHumanInfos)
                {
                    int chanceToAttack = Random.Range(0, 100);
                    int chanceToAttackBar = 0;
                    if (validHumanInfo == heroInfo)
                    {
                        if (humanInfo.isTeamed)
                        {
                            continue;
                        }
                        chanceToAttackBar = AttackChanceWithRelationship[(int)humanInfo.relationDescriptionEnum];
                        chanceToAttackBar -= humanInfo.HowYouBehaveInTheGame;
                    } else
                    {
                        chanceToAttackBar = 30;
                        
                    }
                    chanceToAttackBar = (int)(chanceToAttackBar*attackChanceRatio);
                    if (chanceToAttack> chanceToAttackBar)
                    {
                        humanInfo.Attack(validHumanInfo);

                        humanInfo.remainTargetTransformMovement = 0;
                        if (validHumanInfo == heroInfo)
                        {
                            dialogs.Add(humanInfo.Name + " attacked you.");
                        }
                        acted = true;
                        break ;
                    }
                }
                if (acted)
                {
                    continue;
                }
            }
           
            {
                //let's move
                if (humanInfo.remainTargetTransformMovement > 0 && 
                    Vector3.Distance(humanInfo.transform.position,humanInfo.targetPosition)>=1.5f)
                {
                    //already knows where to move
                    float offsetX = Mathf.Abs(humanInfo.transform.position.x - humanInfo.targetPosition.x);
                    float offsetY = Mathf.Abs(humanInfo.transform.position.y - humanInfo.targetPosition.y);
                    Debug.Log(humanInfo.Name + " at " + humanInfo.transform.position + " before move  " );
                    Vector3 position = humanInfo.transform.position;
                    if (offsetX >= offsetY)
                    {
                        if (humanInfo.targetPosition.x - humanInfo.transform.position.x > 0)
                        {
                            position += new Vector3(1, 0, 0);
                        }
                        else
                        {
                            position += new Vector3(-1, 0, 0);
                        }
                    }
                    humanInfo.gameObject.layer = LayerMask.NameToLayer("Default");
                    Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(position, 1.3f, 1 << LayerMask.NameToLayer("Player"));
                    humanInfo.gameObject.layer = LayerMask.NameToLayer("Player");
                    if (hitColliders2.Length == 0)
                    {
                        humanInfo.transform.position = position;
                    }
                    else
                    {
                        position = humanInfo.transform.position;
                        if (humanInfo.targetPosition.y - humanInfo.transform.position.y > 0)
                        {
                            position += new Vector3(0, 1, 0);
                        }
                        else
                        {
                            position += new Vector3(0, -1, 0);
                        }
                    }
                    humanInfo.gameObject.layer = LayerMask.NameToLayer("Default");
                    Collider2D[] hitColliders3 = Physics2D.OverlapCircleAll(position, 1.3f, 1 << LayerMask.NameToLayer("Player"));
                    humanInfo.gameObject.layer = LayerMask.NameToLayer("Player");
                    if (hitColliders3.Length == 0)
                    {
                        humanInfo.transform.position = position;
                    }



                    Debug.Log(humanInfo.Name + " at " + humanInfo.transform.position + " after move  " + " remain "+ humanInfo.remainTargetTransformMovement);
                    humanInfo.remainTargetTransformMovement -= 1;
                    BRHintManager.Instance.AddHint(humanInfo.transform.position, BRHintEnum.step);
                } else
                {
                    //either move to player or move to a random player
                    int chanceMoveToHero = Random.Range(0, 100);
                    if (chanceMoveToHero > 90)
                    {
                        humanInfo.targetPosition = HumanManager.Instance.heroInfo.transform.position;
                        Debug.Log(humanInfo.Name + " at " + humanInfo.transform.position + " set target position 1 to " + humanInfo.targetPosition);

                    }
                    else if (chanceMoveToHero > 50)
                    {
                        HumanInfo randomInfo = aliveHumans[Random.Range(0, aliveHumans.Count)];
                        humanInfo.targetPosition = randomInfo.transform.position;

                        Debug.Log(humanInfo.Name + " at " + humanInfo.transform.position + " set target position 2 to " + humanInfo.targetPosition);
                    } else
                    {
                        HumanInfo randomInfo = aliveHumans[Random.Range(0, aliveHumans.Count)];
                        Vector3 targetPosition = humanInfo.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                        if (targetPosition.x>=-25 && targetPosition.x<=25 && targetPosition.y >= -15 && targetPosition.y <= 15)
                        {
                            Debug.Log(humanInfo.Name + " at " + humanInfo.transform.position + " set target position 3  to " + targetPosition);
                            humanInfo.targetPosition = targetPosition;
                        }
                    }

                    humanInfo.remainTargetTransformMovement = 10;
                }
            }

            if (DeadZoneManager.Instance.IsInBombedDeadZone(humanInfo.transform.position))
            {
                humanInfo.HurtHuman(1000, "DEAD ZONE");
            }
        }

        //end
        if (!HumanManager.Instance.heroInfo.IsAlive)
        {
            if (aliveHumans.Count == 0)
            {
                BREndManager.Instance.ALLDeadEnd();
            }
            else
            {
                BREndManager.Instance.BadEnd(HumanManager.Instance.heroInfo.killedBy);
            }
        } else
        {
            if (aliveHumans.Count == 0)
            {
                BREndManager.Instance.GoodEnd();
            } else if (aliveHumans.Count == 1)
            {
                foreach(HumanInfo info in aliveHumans)
                {
                    if (info.loverName.Equals(HumanManager.Instance.heroInfo.Name))
                    {
                        BREndManager.Instance.TrueEnd();
                    }
                }
            }
        }

        //other dialogs
        if (dialogs.Count>0)
        {
            DialogManager.CreateViewController(dialogs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

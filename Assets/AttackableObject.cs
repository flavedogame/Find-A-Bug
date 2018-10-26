using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableObject : GameHealthObject
{
    public int attack;
    public int rangeStart;
    public int rangeEnd;
    public bool isControlledByPlayer;
    public GameObject attackEffect;
    public AudioClip  attackSFX;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isControlledByPlayer && 
            AchievementManager.Instance.achievementDictionary["finishWallBugNarration"].state == AchievementState.complete)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeEnd, 1 << LayerMask.NameToLayer("Monsters"));
            foreach (Collider2D hitCollider in hitColliders)
            {
                //Debug.Log("hit " + hitCollider.gameObject);
                GameHealthObject healthScript = hitCollider.GetComponent<GameHealthObject>();
                if (healthScript)
                {
                    AchievementManager.Instance.FinishAchievement("firstTimeAttackMonster");
                    healthScript.GetDamage(attack);
                    Instantiate(attackEffect, hitCollider.gameObject.transform,true);
                    audioSource.clip = attackSFX;
                    audioSource.Play();
                }
            }

            //temp
            //NarrationManager.Instance.ShowNarrationWithIdentifier("v1Dialog");
        }
    }

    void GetDamage()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour, IHealthSystem
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHp=100;
    [SerializeField] private float distanceForWalking;
    [SerializeField] private float distanceForAttack;

    public event System.Action onHpChanged = delegate { };
    public static event System.Action onEnemyDied = delegate { };
    private EnemyBehaviour enemyBehaviour;
    private Animator anim;
    private float currentHP;


    #region Audio
    [SerializeField]
    private AudioClip getHitClip;
    private AudioSource audioSourse;
    #endregion

    private void Awake()
    {
        audioSourse = GetComponent<AudioSource>();
        enemyBehaviour = EnemyBehaviour.idle;
        anim = GetComponent<Animator>();
        currentHP = maxHp;
    }

    private void Update()
    {
        try
        {
            if (enemyBehaviour == EnemyBehaviour.death) return;
            float distance = Vector3.Distance(transform.position, PlayerCharacteristics.POI);
            GetBehaviourWithDistance(distance);
            transform.LookAt(PlayerCharacteristics.POI);

        }
        catch (NullReferenceException)
        {
            // poi was trust  
        }
    }
    public void GetBehaviourWithDistance(float distance)
    {
        try
        {
            if (distance < distanceForAttack)
            {
                if (enemyBehaviour != EnemyBehaviour.attack)
                {
                    anim.SetBool("Walking", false);
                    anim.SetBool("Attack", true);
                }
                enemyBehaviour = EnemyBehaviour.attack;
                return;
            }
            if (distance < distanceForWalking)
            {
                transform.position = Vector3.Lerp(transform.position, PlayerCharacteristics.POI, Time.deltaTime * moveSpeed);
                if (enemyBehaviour != EnemyBehaviour.walking)
                {
                    anim.SetBool("Attack", false);
                    anim.SetBool("Walking", true);
                }
                enemyBehaviour = EnemyBehaviour.walking;
            }
        }
        catch (NullReferenceException)
        {
            // poi was trust  
        }
    }
    public void GetDamge(float damageFor)
    {
        currentHP -= damageFor;
        onHpChanged?.Invoke();
        audioSourse.Play();
        if (currentHP <= 0)
        {
            if (enemyBehaviour != EnemyBehaviour.death)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Attack", false);
                anim.SetBool("Death", true);
            }
            enemyBehaviour = EnemyBehaviour.death;
        }
    }

    public void GiveDamage()
    {
        try
        {
            float distance = Vector3.Distance(transform.position, PlayerCharacteristics.POI);
            if (distance < distanceForAttack)
            {
                PlayerCharacteristics.GetDamage(damage);
            }
        }
        catch (NullReferenceException)
        {
            // poi was trust  
        }
    }
    public void StartDeath()
    {
        Destroy(this.GetComponent<CapsuleCollider>());
        this.gameObject.layer = 10;
    }

    public void EndDeath() 
    {
        onEnemyDied?.Invoke();
        Destroy(this.gameObject);
    }

    public float MaxHeatlh => maxHp;
    public float CurrentHealth => currentHP;
}
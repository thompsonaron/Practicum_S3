﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform playerTargetPos;
    public Animator animator;
    public GameAsset rangedAsset = GameAsset.Bullet;
    public PlayerController playerReference;
    public PlayerHealth playerHealthReference;
    public float meleeDamage = 0.5f;

    public void Start()
    {
        SetupEnemyAI();
    }

    private void SetupEnemyAI()
    {
        playerReference = FindObjectOfType<PlayerController>();
        playerHealthReference = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
        playerTargetPos = FindObjectOfType<PlayerController>().gameObject.transform;
        navMeshAgent.SetDestination(playerTargetPos.position);
    }

    //public void OnEnable()
    //{
    //    SetupEnemyAI();
    //}

    private void Update()
    {
        if (playerTargetPos == null)
        {
            playerTargetPos = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        animator.SetFloat("Distance", Vector3.Distance(transform.position, playerTargetPos.position));
    }
}
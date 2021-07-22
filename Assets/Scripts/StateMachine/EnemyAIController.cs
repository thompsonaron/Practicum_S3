using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyAIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform playerTargetPos;
    public Animator animator;
    public GameAsset rangedAsset = GameAsset.Bullet;
    public PlayerController playerReference;
    public PlayerHealth playerHealthReference;
    public float meleeDamage = 0.5f;
    public bool isFatguy;
    public VisualEffect effOnGround;

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

  

    private void Update()
    {
        if (playerTargetPos == null)
        {
            playerTargetPos = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        animator.SetFloat("Distance", Vector3.Distance(transform.position, playerTargetPos.position));
    }
}

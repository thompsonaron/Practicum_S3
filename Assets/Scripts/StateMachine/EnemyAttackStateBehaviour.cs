using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateBehaviour : StateMachineBehaviour
{
    public EnemyAIController enemyAIController;
    private float attackSpeed = 1f;
    private float attackTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAIController = animator.GetComponent<EnemyAIController>();
        attackTime = attackSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTime -= Time.deltaTime;
        if (attackTime <= 0.0f)
        {
            enemyAIController.playerHealthReference.TakeDamage(enemyAIController.meleeDamage);
            attackTime = attackSpeed;
            if (enemyAIController.isFatguy == true)
            {
                enemyAIController.effOnGround.Play();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

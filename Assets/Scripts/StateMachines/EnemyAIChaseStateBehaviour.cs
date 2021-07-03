using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIChaseStateBehaviour : StateMachineBehaviour
{
    NavMeshAgent enemyAINavMesh;
    EnemyAIController enemyAIController;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAIController = animator.GetComponent<EnemyAIController>();
        enemyAINavMesh = animator.GetComponent<NavMeshAgent>();
        enemyAINavMesh.isStopped = false;
        if (enemyAIController.playerTargetPos != null)
        {
            enemyAINavMesh.SetDestination(enemyAIController.playerTargetPos.position);
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // TODO can change logic into Vector3.Distance > 0.1f
        if (enemyAINavMesh.destination != enemyAIController.playerTargetPos.position)
        {
            enemyAINavMesh.SetDestination(enemyAIController.playerTargetPos.position);
            enemyAIController.transform.LookAt(enemyAIController.playerTargetPos.position);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAINavMesh.isStopped = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
  NavMeshAgent agent;
  AttackController attackController;
  public float stopAttackingDistance = 1.2f;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    agent = animator.GetComponent<NavMeshAgent>();
    attackController = animator.GetComponent<AttackController>();
    attackController.SetAttackMaterial();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    if (attackController.targetToAttack != null && animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
    {
      LookAtPlayer();
      agent.SetDestination(attackController.targetToAttack.position);

      var damageToInflict = attackController.unitDamage;

      // Actually Attack Unit
      attackController.targetToAttack.GetComponent<Enemy>().ReceiveDamage(damageToInflict);

      // Should Unit Transition to Attack State?
      float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
      if (distanceFromTarget > stopAttackingDistance || attackController.targetToAttack == null)
      {

        animator.SetBool("isAttacking", false); // Move to Attacking state
      }
    }
  }

  private void LookAtPlayer()
  {
    Vector3 direction = attackController.targetToAttack.position - agent.transform.position;
    agent.transform.rotation = Quaternion.LookRotation(direction);

    var yRotation = agent.transform.eulerAngles.y;
    agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {

  }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FSMStates
{

    public AttackState(Transform[] wp)
    {
        wayPoints = wp;
        stateID = FSMStateID.Attacking;

        currentRotSpeed = 5.0f;
        currentSpeed = 0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        destPos = player.position;

        float dist = Vector3.Distance(npc.position, destPos);
        if (dist >= 3.2f)
        {
            Debug.Log("Switched to Chase state");
            npc.GetComponent<MinionController>().SetTransition(Transition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        
        anim.SetBool("isAttacking", true);

        Quaternion targetRotation = Quaternion.LookRotation(player.position - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);

        float dist = Vector3.Distance(npc.position, player.position);

        if (dist >= 2f)
        {
            anim.SetBool("isAttacking", false);
        }
        

    }



}

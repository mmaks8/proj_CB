using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MLAttack : MLStatesID
{   
    public MLAttack(Transform[] wp)
    {
        wayPoints = wp;
        stateID = MLStateID.Attacking;

        currentRotSpeed = 5.0f;
        currentSpeed = 0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        destPos = player.position;

        float dist = Vector3.Distance(npc.position, destPos);
        if (dist >= 8f)
        {
            Debug.Log("Switched to Chase state");
            npc.GetComponent<MLController>().SetTransition(MLTransition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.position - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);
        float dist = Vector3.Distance(npc.position, player.position);

        if(dist <= 8f)
        {
            npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);
            npc.GetComponent<MLController>().ShootProjectile();
            nav.stoppingDistance = 7f;
            anim.speed = 1f;
            anim.SetBool("isAttacking", true);
            // damage player
        }

        else if (dist >= 8f)
        {
            anim.SetBool("isAttacking", false);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : FSMStates
{
    

    public PatrolState(Transform[] wp)
    {
        wayPoints = wp;
        stateID = FSMStateID.Patrolling;

        currentRotSpeed = 1.0f;
        currentSpeed = 1.4f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //transitions to chase state when player is detected
        if (Vector3.Distance(npc.position, player.position) <= 8.0f)
        {
            Debug.Log("Switched to Chase State");
            npc.GetComponent<MinionController>().SetTransition(Transition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        nav.speed = 2f;

        //anim.SetBool("Move Forward Slow", true);

        //finds another waypoint if one is already reached

        if (Vector3.Distance(npc.position, destPos) <= 2.0f)
        {
            Debug.Log("Reached destination. Moving to next destination.");
            FindNextPoint();
        }

        nav.SetDestination(destPos);
    }
}

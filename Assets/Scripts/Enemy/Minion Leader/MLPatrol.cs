using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MLPatrol: MLStatesID
{


    public MLPatrol(Transform[] wp)
    {
        wayPoints = wp;
        stateID = MLStateID.Patrolling;

        currentRotSpeed = 1.0f;
        currentSpeed = 1.4f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //transitions to chase state when player is detected
        if (Vector3.Distance(npc.position, player.position) <= 8.0f)
        {
            Debug.Log("Switched to Chase State");
            npc.GetComponent<MLController>().SetTransition(MLTransition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        nav.speed = 2f;

        //finds another waypoint if one is already reached

        if(destPos == Vector3.zero)
        {
            Debug.Log("Reached destination. Moving to next destination.");
            FindNextPoint();
        }

        if (Vector3.Distance(npc.position, destPos) <= 2.0f)
        {
            Debug.Log("Reached destination. Moving to next destination.");
            FindNextPoint();
        }

        nav.SetDestination(destPos);
    }
}


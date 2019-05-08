using UnityEngine;
using UnityEngine.AI;

public class BossPatrol: BossStatesID
{
    public BossPatrol(Transform[] wp)
    {
        wayPoints = wp;
        stateID = BossStateID.Patrolling;

        currentRotSpeed = 1.0f;
        currentSpeed = 1.4f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //transitions to chase state when player is detected
        float dist = Vector3.Distance(npc.position, player.position);
        if (dist <= 7.0f)
        {
            npc.GetComponent<BossController>().SetTransition(BossTransition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        nav.speed = 2f;

        //finds another waypoint if one is already reached

        if (Vector3.Distance(npc.position, destPos) <= 2.0f)
        {
            Debug.Log("Reached destination. Moving to next destination.");
            FindNextPoint();
        }

        nav.SetDestination(destPos);
    }
}


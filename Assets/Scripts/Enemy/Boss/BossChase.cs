﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossChase : BossStatesID
{
    public BossChase(Transform[] wp)
    {
        wayPoints = wp;
        stateID = BossStateID.Chasing;

        currentRotSpeed = 1.0f;
        currentSpeed = 2.2f;

        //find next waypoint position
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {

        destPos = player.position;

        //switches to attack state when closer to the target
        float dist = Vector3.Distance(npc.position, destPos);
        if (dist <= CONSTANTS.ENEMY.BOSS.ATTACK_DISTANCE)
        {
            Debug.Log("Switched to Attack state");
            npc.GetComponent<BossController>().SetTransition(BossTransition.ReachedPlayer);

        }
        //switches to patrol state if target is too far
        else if (dist >= CONSTANTS.ENEMY.BOSS.PATROL_DISTANCE)
        {
            Debug.Log("Switched to Patrol state");
            npc.GetComponent<BossController>().SetTransition(BossTransition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_ATTACK, false);
        anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_RUN, true);

        destPos = player.position;

        nav.SetDestination(destPos);
    }
}


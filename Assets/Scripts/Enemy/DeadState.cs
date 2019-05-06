using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : FSMStates
{
    public DeadState()
    {
        stateID = FSMStateID.Dead;
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        
    }

    public override void Reason(Transform player, Transform npc)
    {
        
    }
}

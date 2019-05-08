using UnityEngine;
using UnityEngine.AI;

public class BossDead : BossStatesID
{
    public BossDead()
    {
        stateID = BossStateID.Dead;   
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        
    }

    public override void Reason(Transform player, Transform npc)
    {
        
    }
}

using UnityEngine;
using UnityEngine.AI;

public class BossAttack : BossStatesID
{   
    private float fireRate = CONSTANTS.ENEMY.BOSS.FIRE_RATE;
    private float nextFire = 0.0f;
    public BossAttack(Transform[] wp)
    {
        wayPoints = wp;
        stateID = BossStateID.Attacking;

        currentRotSpeed = 5.0f;
        currentSpeed = 0.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        destPos = player.position;

        float dist = Vector3.Distance(npc.position, destPos);
        if (dist >= 8f)
        {
            Debug.Log("Switched to Chase state");
            npc.GetComponent<BossController>().SetTransition(BossTransition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_ATTACK, true);

            Quaternion targetRotation = Quaternion.LookRotation(player.position - npc.position);
            npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);

            float dist = Vector3.Distance(npc.position, player.position);

            if (dist >= 4f)
            {
                anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_ATTACK, false);
                anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_RUN, true);
            }            
        }
    }
}

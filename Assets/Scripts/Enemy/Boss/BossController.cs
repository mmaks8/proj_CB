using UnityEngine;
using UnityEngine.AI;

public class BossController : BossFSM
{
    private Animator anim;

    private NavMeshAgent nav;

    public Transform spawnPoint;

    public GameObject projectile;

    public float damage;

    public float hp;

    private float elapsedTime;

    private float shootRate;

    private bool isAlive;

    protected override void Initialize()
    {
        isAlive = true;
        hp = CONSTANTS.ENEMY.BOSS.HP;
        damage = CONSTANTS.ENEMY.BOSS.DAMAGES;
        elapsedTime = 0.0f;
        shootRate = 3f;

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.speed = 1.0f;
        //get the tag 'Player'
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        //display error if Player tag is missing
        if (!playerTransform)
            print("No player detected. Add a 'Player' tag.");

        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        if (isAlive)
        {
            CurrentState.Reason(playerTransform, transform);
            CurrentState.Act(playerTransform, transform, anim, nav, damage);            
        }
    }

    public void SetTransition(BossTransition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        pointList = GameObject.FindGameObjectsWithTag("BossPoint");

        Transform[] wayPoints = new Transform[pointList.Length];

        int i = 0;

        foreach (GameObject obj in pointList)
        {
            wayPoints[i] = obj.transform;
            i++;
        }

        BossPatrol patrol = new BossPatrol(wayPoints);
        patrol.AddTransition(BossTransition.FoundPlayer, BossStateID.Chasing);
        patrol.AddTransition(BossTransition.NoHP, BossStateID.Dead);

        BossChase chase = new BossChase(wayPoints);
        chase.AddTransition(BossTransition.LostPlayer, BossStateID.Patrolling);
        chase.AddTransition(BossTransition.ReachedPlayer, BossStateID.Attacking);
        chase.AddTransition(BossTransition.NoHP, BossStateID.Dead);

        BossAttack attack = new BossAttack(wayPoints);
        attack.AddTransition(BossTransition.FoundPlayer, BossStateID.Chasing);
        attack.AddTransition(BossTransition.NoHP, BossStateID.Dead);

        BossDead dead = new BossDead();
        dead.AddTransition(BossTransition.NoHP, BossStateID.Dead);

        AddBossState(patrol);
        AddBossState(chase);
        AddBossState(attack);
        AddBossState(dead);
        
        anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_RUN, true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Debug.Log("------------- collision " + hp);
                anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_TAKE_DAMAGE, true);
                hp -= 25;

                if (hp <= 0)
                {
                    // Stop all the animations
                    foreach (AnimatorControllerParameter animation in anim.parameters)
                    {
                        anim.SetBool(animation.name, false);
                    }

                    anim.SetBool(CONSTANTS.ENEMY.BOSS.ANIM_DIE, true);
                    SetTransition(BossTransition.NoHP);
                    Destroy(gameObject, CONSTANTS.GLOBAL.TIME_BEFORE_DESTROY); // Destroy the body after 'TINE_BEFORE_DESTROY' sec
                    isAlive = false;
                    elapsedTime = 0f;
                }
            }            
        }
    } 
}


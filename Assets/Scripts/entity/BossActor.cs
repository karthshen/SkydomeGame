using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossActor : EnemyActor
{
    public List<TrollHand> weapons;

    private ActorState defaultState;

    public GameObject endofGameSign;

    // Use this for initialization
    void Start()
    {
        defaultState = new EnemyStandingState();
        state = defaultState;

        ac = GetComponent<AnimatorController>();
        rb = GetComponent<Rigidbody>();

        if (ActorData == null)
            ActorData = new BullBossData();

        BIsGrounded = true;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
        skydomeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if (!Player || !skydomeCamera)
        {
            throw new MissingReferenceException();
        }

        foreach(TrollHand weapon in weapons)
        {
            weapon.ItemPickup(this);
        }

        EffectSource = GetComponent<AudioSource>();

        InitializeActor();

        this.FREEZEING_TIME_DEFAULT = 0.50f;
        this.attackTimerMagicNumber = 0f;

        ATTACK_RANGE = 4f;

        endofGameSign.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Actor Methods

    public override void Move()
    {
        float step = ActorData.MoveVelocity * Time.deltaTime; // calculate distance to move
        Vector3 newPosition = Vector3.MoveTowards(transform.position, Player.transform.position, step);

        if (newPosition.x - transform.position.x > 0)
        {
            MoveHorizontal = 1f; //Moving to right
        }
        else
        {
            MoveHorizontal = -1f; //Moving to left
        }

        TurnAround();

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        //base.Move();
    }
    public override void Attack()
    {
        string AttackAnimation1 = "animation,23";
        string AttackAnimation2 = "animation,24";
        string AttackAnimation3 = "animation,25";

        List<string> attackAnimation = new List<string>();

        attackAnimation.Add(AttackAnimation1);
        attackAnimation.Add(AttackAnimation2);
        attackAnimation.Add(AttackAnimation3);

        if (AttackTimer <= 0)
        {
            AttackCode = System.Guid.NewGuid();
            //Debug.Log("AttackCode: " + actor.AttackCode);
            AttackTimer = 1.2f;

            ActorData.AttackAnimation1 = attackAnimation[Random.Range(0,3)];

            foreach(TrollHand weapon in weapons) {
                weapon.UseItem(this);
            }
        }
    }

    protected override void BackToStanding()
    {
        if (state.GetType() != typeof(ActorDeathState))
        {
            state = new EnemyStandingState();
            AttackTimer = 0f;
            ((ActorState)(state)).PlayStateAnimation(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.ActorUpdate();

        if (IsPlayerInAggroRange() && !engagedCombat)
        {
            EngageCombat();
        }

        if (state != null)
        {
            state = state.HandleInput(this, null);
        }

        if(endofGameSign.GetComponent<SpriteRenderer>().enabled && DeathTimer <=0)
        {
            Application.Quit();
        }
    }

    private bool IsPlayerInAggroRange()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 7)
        {
            return true;
        }

        return false;
    }

    public override void Death()
    {
        base.Death();
        DeathTimer = 6f;
        this.GetComponent<Collider>().enabled = false;
        this.rb.isKinematic = true;

        endofGameSign.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void EngageCombat()
    {
        skydomeCamera.COMBAT_CAMERA_DISTANCE1 = 10f;
        skydomeCamera.ActorSpottedInCamera(this);
        
        engagedCombat = true;
    }

    protected override void AfterDeath()
    {
        skydomeCamera.actors.Remove(this);
        Destroy(gameObject);
    }
}

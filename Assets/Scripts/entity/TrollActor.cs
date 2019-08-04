using UnityEngine;
using System.Collections;

public class TrollActor : EnemyActor
{
    public TrollHand weapon;

    private ActorState defaultState;

    // Use this for initialization
    void Start()
    {
        defaultState = new EnemyStandingState();
        state = defaultState;

        ac = GetComponent<AnimatorController>();
        rb = GetComponent<Rigidbody>();

        if (ActorData == null)
            ActorData = new TrollData();

        BIsGrounded = true;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
        skydomeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if(!Player || !skydomeCamera)
        {
            throw new MissingReferenceException();
        }

        if(weapon)
        {
            weapon.ItemPickup(this);
        }

        InitializeActor();

        this.FREEZEING_TIME_DEFAULT = 0.50f;
        this.attackTimerMagicNumber = 0f;
    }

    //Actor Methods

    public override void Move()
    {
        float step = ActorData.MoveVelocity * Time.deltaTime; // calculate distance to move
        Vector3 newPosition = Vector3.MoveTowards(transform.position, Player.transform.position, step);

        if(newPosition.x - transform.position.x > 0)
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
        if (AttackTimer <= 0)
        {
            AttackCode = System.Guid.NewGuid();
            //Debug.Log("AttackCode: " + actor.AttackCode);
            AttackTimer = 1.5f;
            weapon.UseItem(this);
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

        if(IsPlayerInAggroRange() && !engagedCombat)
        {
            EngageCombat();
        }

        if(state != null)
        {
            state = state.HandleInput(this, new InControl.InputDevice());
        }
    }

    private bool IsPlayerInAggroRange()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 10)
        {
            return true;
        }

        return false;
    }

    private void EngageCombat()
    {
        skydomeCamera.ActorSpottedInCamera(this);
        engagedCombat = true;
    }

    protected override void AfterDeath()
    {
        skydomeCamera.actors.Remove(this);
        Destroy(gameObject);
    }
}

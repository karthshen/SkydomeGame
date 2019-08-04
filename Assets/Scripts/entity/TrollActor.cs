using UnityEngine;
using System.Collections;

public class TrollActor : EnemyActor
{
    private ActorState defaultState;
    private AActor player;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
        skydomeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if(!player || !skydomeCamera)
        {
            throw new MissingReferenceException();
        }

        InitializeActor();

        this.FREEZEING_TIME_DEFAULT = 0.50f;
    }

    //Actor Methods

    public override void Move()
    {
        float step = ActorData.MoveVelocity * Time.deltaTime; // calculate distance to move
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.transform.position, step);

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

    protected override void BackToStanding()
    {
        if (state.GetType() != typeof(ActorDeathState))
        {
            state = new EnemyStandingState();
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
        if (Vector3.Distance(player.transform.position, this.transform.position) < 10)
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

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void AfterDeath()
    {
        skydomeCamera.actors.Remove(this);
        Destroy(gameObject);
    }
}

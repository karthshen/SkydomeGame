using UnityEngine;
using System.Collections;

public class ArcherActor : AActor
{
    ActorState defaultState;

    public ArcherBow archerBow;

    public ArcherActor() : base()
    {

    }

    private void Start()
    {
        defaultState = new ActorStandingState();
        state = defaultState;

        ac = GetComponent<AnimatorController>();
        rb = GetComponent<Rigidbody>();

        //Actor Config, button, ability, etc
        if(ActorData == null)
            ActorData = new ArcherData();

        BIsGrounded = true;

        if (archerBow)
        {
            archerBow.ItemPickup(this);
        }

        InitializeActor();
    }

    public override void Attack()
    {
        AttackCode = System.Guid.NewGuid();
        //Debug.Log("AttackCode: " + actor.AttackCode);
        AttackTimer = AActor.ATTACK_TIMER;

        archerBow.UseItem(this);
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void Move()
    {
        base.Move();
    }

    //MonoBehavior Functions
    private void Update()
    {
        base.ActorUpdate();
    }

    protected override void AfterDeath()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;
using System.Collections;

public class ArcherActor : AActor
{
    ActorState defaultState;
    string actorName = "Archer";

    //public ArcherBow bow;

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

        InitializeActor();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
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
}

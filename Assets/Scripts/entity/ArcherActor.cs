using UnityEngine;
using System.Collections;

public class ArcherActor : AActor
{
    ActorState defaultState;

    public ArcherBow archerBow;

    [SerializeField]
    private float energyRegenSpeed = 5f;
    [SerializeField]
    private float attackEnergyCost = 10f;

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

        abilityUp = new ArcherBurstShot(this);
        abilityTrigger = new ArcherShootClawhook(this);

        if (archerBow)
        {
            archerBow.ItemPickup(this);
        }

        InitializeActor();
    }

    public override void Attack()
    {
        if (CurrentEnergy >= attackEnergyCost)
        {
            CurrentEnergy -= attackEnergyCost;

            AttackCode = System.Guid.NewGuid();
            //Debug.Log("AttackCode: " + actor.AttackCode);
            AttackTimer = AActor.ATTACK_TIMER;

            archerBow.UseItem(this);
        }
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

        if(CurrentEnergy<=100)
        {
            CurrentEnergy += energyRegenSpeed * Time.deltaTime;
        }
    }

    protected override void AfterDeath()
    {
        throw new System.NotImplementedException();
    }
}

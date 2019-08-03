using UnityEngine;
using System.Collections;

public class TrollActor : AActor
{
    private ActorState defaultState;
    private AActor player;
    private CameraController camera;

    private bool engagedCombat = false;

    // Use this for initialization
    void Start()
    {
        defaultState = new ActorStandingState();
        state = defaultState;

        ac = GetComponent<AnimatorController>();
        rb = GetComponent<Rigidbody>();

        if (ActorData == null)
            ActorData = new TrollData();

        BIsGrounded = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if(!player || !camera)
        {
            throw new MissingReferenceException();
        }

        InitializeActor();

        this.FREEZEING_TIME_DEFAULT = 0.25f;
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
            state.HandleInput(this, new InControl.InputDevice());
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
        camera.ActorSpottedInCamera(this);
        engagedCombat = true;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void AfterDeath()
    {
        camera.actors.Remove(this);
        Destroy(gameObject);
    }
}

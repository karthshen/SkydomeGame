using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public abstract class AActor : MonoBehaviour
{
    // Constants
    public const float ATTACK_TIMER_INTERVAL = 0.5f;
    public float AIR_ATTACK_LENGTH = 0.35f / 1.3f;
    public const float CAST_DURATION = 0.5f;
    private const float FREEZEING_TIME_DEFAULT = 1.0f / 1000f * 85f;

    private bool bIsGrounded = false;
    private Vector3 frontDirection = new Vector3(0, 90, 0);
    private Vector3 backDirection = new Vector3(0, 270, 0);

    //Protected Attributes
    protected ActorState state;

    protected Rigidbody rb;
    protected AnimatorController ac;
    //Private Attributes
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentEnergy;
    [SerializeField]
    private float moveHorizontal;
    [SerializeField]
    private float jumpForceFactor = 91f;

    private int jumpNum = 0;

    private ActorData actorData;

    private float freezeTimer = 0f;
    //Abilities

    //Timer

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float CurrentEnergy { get => currentEnergy; set => currentEnergy = value; }
    public float MoveHorizontal { get => moveHorizontal; set => moveHorizontal = value; }
    public ActorState State { get => state; set => state = value; }
    public bool BIsGrounded { get => bIsGrounded; set => bIsGrounded = value; }
    public int JumpNum { get => jumpNum; set => jumpNum = value; }
    public float FreezeTimer { get => freezeTimer; set => freezeTimer = value; }
    public ActorData ActorData { get => actorData; set => actorData = value; }

    //Functionalities
    protected void InitializeActor()
    {
        //TODO: initialize state to standing state

        if (rb)
        {
            rb.isKinematic = false;
        }

        CurrentHealth = ActorData.MaxHealth;
        CurrentEnergy = ActorData.MaxEnergy;
    }

    public virtual void Move()
    {
        TurnAround();
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);
        transform.Translate(movement * ActorData.MoveVelocity * Time.deltaTime);
    }

    public virtual void Jump()
    {
        ClearForceOnActor();
        if (rb.useGravity == false || rb.velocity != Vector3.zero || JumpNum < 0)
            return;

        Vector3 forceJump = Vector3.up * ActorData.JumpVelocity * jumpForceFactor;

        if (JumpNum == 0)
        {
            forceJump *= 1.1f;
        }

        rb.AddForce(forceJump, ForceMode.Acceleration);
    }

    public abstract void Attack();

    public virtual void Death()
    {
        //Set state to death state

        //Respawn for Player
    }

    public virtual float TakeDamage(float damage, AActor attacker)
    {
        if (state.GetType() == typeof(ActorDeathState))
            return 0;

        if (CurrentHealth <= 0.001)
        {
            CurrentHealth = 0;
            Death();
        }
        else
        {
            freezeTimer = 0;
            if (state.GetType() != typeof(ActorDeathState))
                state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
        }

        return CurrentHealth;
    }

    public virtual void KnockBack(float knockingForce, AActor attacker)
    {
        KnockBackBasedOnPosition(knockingForce, attacker);
    }

    private void KnockBackBasedOnPosition(float knockingForce, AActor attacker)
    {
        float leftDirectionInRadian = 270f * Mathf.PI / 180;
        float rightDirectionInRadian = 90f * Mathf.PI / 180;

        Vector3 backMovement;

        if (transform.position.x < attacker.transform.position.x)
        {
            backMovement = new Vector3(knockingForce * Mathf.Sin(leftDirectionInRadian), 0f, 0f);
        }
        else if (transform.position.x > attacker.transform.position.x)
        {
            backMovement = new Vector3(knockingForce * Mathf.Sin(rightDirectionInRadian), 0f, 0f);
        }
        else
        {
            backMovement = new Vector3(0f, 0f, 0f);
        }

        GetRigidbody().AddForce(backMovement);

        if (state.GetType() != typeof(ActorDeathState))
            state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
    }


    public void HandleInput(InputDevice inputDevice)
    {
        ActorState newState = State.HandleInput(this, inputDevice);
        if (State != null)
        {
            State = newState;
        }
    }

    public bool IsActorAlive()
    {
        return true;
    }

    public void ClearForceOnActor()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public AnimatorController GetAnimatorController()
    {
        return ac;
    }

    private void TurnAround()
    {
        if (moveHorizontal > 0)
        {
            transform.GetChild(0).eulerAngles = frontDirection;
        }
        else if (moveHorizontal < 0)
        {
            transform.GetChild(0).eulerAngles = backDirection;
        }
    }

    protected virtual void BackToStanding()
    {
        if (state.GetType() != typeof(ActorDeathState))
        {
            state = new ActorStandingState();
            ((ActorState)(state)).PlayStateAnimation(this);
        }
    }

    protected void ActorUpdate()
    {

        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                freezeTimer = 0;
                BackToStanding();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == false && state.GetType() != typeof(ActorDeathState) && state.GetType() != typeof(ActorFreezeState))
        {
            bIsGrounded = true;
            BackToStanding();
            //Debug.Log("Entering StandingState from Ground");
        }
    }
}

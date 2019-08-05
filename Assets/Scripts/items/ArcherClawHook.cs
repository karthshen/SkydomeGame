﻿using UnityEngine;

public class ArcherClawHook : ProjectileItem
{
    enum ClawhookState
    {
        Shooting,
        RetractingActor,
        RetractingOthers,
        Staying,
        Retracted,
    }

    [SerializeField]
    private float retractionForce = 70f;
    private float velocity = 20.0f;
    private float moveConstant = 1f;
    private float positionHorizontal = 0.05f;
    private float angleJoystick = 0f;
    private bool hasPlayed = false;
    private Vector3 rootPosition;

    private AActor hitActor;

    private Vector3 movement;

    private Animator animator;

    private AudioSource audioSource;

    private const float SHOOT_OUT_DURATION = 1f;
    private const float CLAW_STAY_TIME = 1.0f;
    private const float MAX_TRAVEL_DISTANCE = 9.0f;

    private float timer = 0f;

    private ClawhookState clawhookState;

    private LineRenderer lineRenderer;

    public AActor GetOwner()
    {
        return Owner;
    }

    public override void ProjectileStart()
    {
        IgnoreOwnerCollision(Owner);

        lineRenderer = GetComponent<LineRenderer>();

        SetClawPositionToOwner();

        UpdateRootPosition();

        movement = new Vector3(moveConstant * velocity * Mathf.Cos(angleJoystick), moveConstant * velocity * Mathf.Sin(angleJoystick), 0f);

        clawhookState = ClawhookState.Shooting;

        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0f, 0f, angleJoystick * Mathf.Rad2Deg));

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        PlayAnimation();
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        UpdateRootPosition();

        PlayAnimation();

        DrawLine();

        float distance = Vector3.Distance(transform.position, Owner.transform.position);

        if (distance > MAX_TRAVEL_DISTANCE)
        {
            ProjectileFinish();
        }

        if (clawhookState == ClawhookState.Shooting)
        {
            if (timer > SHOOT_OUT_DURATION)
            {
                ProjectileFinish();
            }

            if (distance > MAX_TRAVEL_DISTANCE)
            {
                ProjectileFinish();
            }

            transform.Translate(movement * Time.deltaTime);
            timer += Time.deltaTime;
        }
        else if (clawhookState == ClawhookState.RetractingActor)
        {
            SetClawPositionToActor(hitActor);
            //push actor toward this actor
            Owner.GetRigidbody().AddForce((transform.position - Owner.transform.position) * retractionForce / distance);

            //Owner.GetRigidbody().AddForce((transform.position - Owner.transform.position) * retractionForce / (1 / Time.deltaTime));
        }
        else if (clawhookState == ClawhookState.RetractingOthers)
        {
            //push actor toward the wall
            Owner.GetRigidbody().AddForce((transform.position - Owner.transform.position) * retractionForce / distance);
            //Owner.GetRigidbody().AddForce((transform.position - Owner.transform.position) * retractionForce / (1 / Time.deltaTime));
        }

        if (clawhookState == ClawhookState.RetractingActor || clawhookState == ClawhookState.RetractingOthers)
        {
            timer += Time.deltaTime;
            if (timer >= CLAW_STAY_TIME)
            {
                ProjectileFinish();
            }
        }

        if (CheckIfRetracted())
            ProjectileFinish();
    }

    private bool CheckIfRetracted()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, Owner.transform.position)) < 1.3f && (clawhookState == ClawhookState.RetractingActor || clawhookState == ClawhookState.RetractingOthers))
        {
            Owner.ClearForceOnActor();

            if (clawhookState == ClawhookState.RetractingActor)
            {
                hitActor.ClearForceOnActor();
            }

            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AEntity>() && !collision.gameObject.GetComponent<AActor>())
        {
            IgnoreGameobjectCollision(collision.gameObject);
            return;
        }
        else if (collision.gameObject.GetComponent<AActor>())
        {
            clawhookState = ClawhookState.RetractingActor;
            hitActor = collision.gameObject.GetComponent<AActor>();
            timer = 0f;


        }
        else
        {
            clawhookState = ClawhookState.RetractingOthers;
            timer = 0f;


        }
    }

    private void UpdateRootPosition()
    {
        rootPosition = new Vector3(Owner.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            Owner.transform.position.y + Owner.transform.lossyScale.y / 2, Owner.transform.position.z);
    }

    private void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, rootPosition);
    }

    private void SetClawPositionToOwner()
    {
        //float yDirectionInRadian = Owner.GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(Owner.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            Owner.transform.position.y + Owner.transform.lossyScale.y / 2, Owner.transform.position.z);
    }

    private void SetClawPositionToActor(AActor hitActor)
    {
        gameObject.transform.position = new Vector3(hitActor.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            hitActor.transform.position.y + hitActor.transform.lossyScale.y / 2, hitActor.transform.position.z);
    }

    public bool CanShootClaw(float x, float y)
    {
        if (x != 0.0f || y != 0.0f)
        {
            angleJoystick = Mathf.Atan2(y, x);
            //Debug.Log("Joystick Angle in Radian:" + angleJoystick + " with X: " + x + " Y: " + y);
            return true;
        }
        else
            return false;
    }

    private void PlayAnimation()
    {
        if (clawhookState == ClawhookState.RetractingActor || clawhookState == ClawhookState.RetractingOthers)
        {
            //animator.SetInteger("animation", 2);
            SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.hookback, ref hasPlayed, 0.5f);
        }
        else
        {
            //animator.SetInteger("animation", 1);
        }
    }

    public override void UseItem(AActor actor)
    {
        throw new System.NotImplementedException();
    }
}
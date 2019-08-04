using UnityEngine;
using System.Collections;

public class ArcherArrow : ProjectileItem
{
    public GameObject com;

    private float velocity = 20.0f;
    private float moveHorizontal = -0.06f;
    private Vector3 movement;
    private float yModifier = 0.06f;
    private float xModifier = 0.06f;
    private float damageModifier = 1f;

    private const float DURATION_TIME = 1f;

    private ParticleSystem[] particleSystems;

    private AudioSource audioSource;

    private bool hasPlayed = false;

    private float duration_time = 0f;

    private bool attacked = false;

    public float YModifier
    {
        get
        {
            return yModifier;
        }

        set
        {
            yModifier = value;
        }
    }

    public float DamageModifier
    {
        get
        {
            return damageModifier;
        }

        set
        {
            damageModifier = value;
        }
    }

    public float XModifier
    {
        get
        {
            return xModifier;
        }

        set
        {
            xModifier = value;
        }
    }

    public override void UseItem(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        
    }

    public override void ProjectileStart()
    {
        Start();

        //Ignore Collision of Owner and this
        IgnoreOwnerCollision(Owner);

        SetRotationToEntity(Owner);
        float yDirectionInRadian = GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(Owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            Owner.transform.position.y + yModifier + Owner.transform.lossyScale.y / 2, Owner.transform.position.z - 0.01f);
        movement = new Vector3(1 * Mathf.Sin(yDirectionInRadian), 0.0f, 0.0f);
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            if (!attacked)
            {
                Owner.AttackCode = System.Guid.NewGuid();
            }
            hitActor.TakeDamage(Owner.ActorData.AttackPower / 1.5f * DamageModifier, Owner);
            //SoundManager.instance.PlayEffectWithAudioSource(hitActor.GetAudioSource(), SoundManager.instance.arrowHit, ref hasPlayed, 0.6f);
            attacked = true;

            ProjectileFinish();
        }

        ProjectileFinish();
    }

    private void Update()
    {
        transform.Translate(movement * velocity * Time.deltaTime);
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ProjectileFinish();
    }
}

using UnityEngine;
using System.Collections;

public class TrollHand : Item 
{
    public override void ItemPickup(AActor actor)
    {
        IgnoreOwnerCollision(Owner);
        GetComponent<Collider>().enabled = true;
        base.ItemPickup(actor);
    }

    public override void UseItem(AActor actor)
    {
        GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        /*
        if (Owner.State.GetType() != typeof(EnemyAttackState))
        {
            GetComponent<Collider>().enabled = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor attackedActor = collision.gameObject.GetComponent<AActor>();
        if (attackedActor && attackedActor.GetInstanceID() != Owner.GetInstanceID())
        {
            attackedActor.TakeDamage(Owner.ActorData.AttackPower, Owner);
        }
    }
}

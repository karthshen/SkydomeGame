using UnityEngine;
using System.Collections;

public class SavePointStar : Item
{
    public override void UseItem(AActor actor)
    {
        actor.Respawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        AActor player = other.gameObject.GetComponent<AActor>();

        if(player && !Owner)
        {
            ItemPickup(player);
            player.CurrentHealth = player.ActorData.MaxHealth;
            player.CurrentEnergy = player.ActorData.MaxEnergy;
            ((ArcherActor)player).savePoint = this;
        }
    }
}

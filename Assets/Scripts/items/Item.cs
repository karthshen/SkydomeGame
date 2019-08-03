using UnityEngine;
using System.Collections;

public abstract class Item : AEntity
{
    private AActor owner;

    protected AActor Owner { get => owner;}

    public virtual void ItemPickup(AActor actor)
    {
        if (Owner == null)
        {
            owner = actor;
        }
        else
        {
            return;
        }
    }

    public abstract void UseItem(AActor actor);

    public virtual void RemoveItem()
    {
        Destroy(gameObject);
    }
}

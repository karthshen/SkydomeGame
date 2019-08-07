using UnityEngine;
using System.Collections;

public class SpikeTrap : Item
{
    public override void UseItem(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        AActor actor = other.gameObject.GetComponent<AActor>();

        if(actor)
        {
            actor.Death();
        }
    }
}

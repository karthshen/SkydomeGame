using UnityEngine;
using System.Collections;

public class FireBurst : Item
{
    private float timer = 2f;

    public override void UseItem(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    private void ParticleFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            ParticleFinish();
    }
}

using UnityEngine;
using System.Collections;

public class ArcherBow : Item
{
    private GameObject archerArrow;

    private float intervalTimer = 0f;
    private float arrowPostponedTimer = 1f;

    private bool arrowReady = true;

    private const float ARROW_POSTPONE_TIME = 0.15f;
    private const float ARROW_INTERVAL = 0.5f;

    public override void UseItem(AActor actor)
    {
        ShootArrow();
    }

    private void ShootArrow()
    { 
        archerArrow = Object.Instantiate(Resources.Load("ArcherArrow") as GameObject);
        ArcherArrow arrow = archerArrow.GetComponent<ArcherArrow>();
        arrow.ItemPickup(Owner);
        arrow.ProjectileStart();
        arrowPostponedTimer = 1f;  
    }
}

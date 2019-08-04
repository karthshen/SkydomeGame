using UnityEngine;
using UnityEditor;

public class ArcherShootClawhook : Ability
{
    private const int MAX_NUM_CLAW = 1;

    GameObject clawhook;

    public ArcherShootClawhook(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 10;
        DragInAir = false;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsClawShootable())
            return;

        caster.CurrentEnergy -= AbilityCost;
        caster.ClearForceOnActor();

        clawhook = Object.Instantiate(Resources.Load("Clawhook")) as GameObject;
        ArcherClawHook claw = clawhook.GetComponent<ArcherClawHook>();

        if (!claw.CanShootClaw(caster.MoveHorizontal, caster.MoveVertical))
        {
            claw.ProjectileFinish();
        }

        claw.ItemPickup(caster);
        claw.ProjectileStart();
    }

    private bool IsClawShootable()
    {
        ArcherClawHook[] claws = GameObject.FindObjectsOfType<ArcherClawHook>();

        int counter = 0;

        foreach (ArcherClawHook claw in claws)
        {
            if (claw.GetOwner().GetInstanceID() == caster.GetInstanceID())
            {
                counter++;
            }
        }

        if (counter >= MAX_NUM_CLAW)
        {
            return false;
        }
        return true;
    }
}
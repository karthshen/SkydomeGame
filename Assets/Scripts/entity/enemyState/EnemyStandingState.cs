using UnityEngine;
using System.Collections;
using InControl;

public class EnemyStandingState : ActorStandingState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        EnemyActor enemy = (EnemyActor)actor;

        PlayAnimation(actor);

        ActorStandingInitialize(actor);

        if (enemy.IsEngagedInCombat())
        {
            return new EnemyMovingState();
        }
        else
        {
            return this;
        }
    }
}

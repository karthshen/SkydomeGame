using UnityEngine;
using System.Collections;
using InControl;

public class EnemyAttackState : ActorAttackState
{
    public EnemyAttackState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        actor.Attack();
        PlayAnimation(actor);

        EnemyActor thisActor = (EnemyActor)actor;

        if (!thisActor.IsAttackInRange())
            return new EnemyMovingState();

        return this;
    }
}

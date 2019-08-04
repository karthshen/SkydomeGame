using UnityEngine;
using System.Collections;
using InControl;

public class EnemyMovingState : ActorState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        AActor player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
        EnemyActor thisActor = (EnemyActor)actor;

        if(thisActor.IsAttackInRange())
        {
            //Attack
            return new EnemyAttackState();
        }
        else
        {
            actor.Move();
            PlayAnimation(actor);
            return this;
        }
    }

    protected override void PlayAnimation(AActor actor)
    {
        /*
        if (Mathf.Abs(actor.MoveHorizontal) < 0.6f)
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.WalkAnimation);
        }
        else
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.RunningAnimation);
        }
        */

        actor.GetAnimatorController().SetInt(actor.ActorData.WalkAnimation);
    }
}

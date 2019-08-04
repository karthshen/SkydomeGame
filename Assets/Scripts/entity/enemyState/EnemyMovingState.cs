using UnityEngine;
using System.Collections;
using InControl;

public class EnemyMovingState : ActorState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        AActor player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();

        if(Vector3.Distance(player.transform.position, actor.transform.position) < 3)
        {
            //Attack
            return new EnemyStandingState();
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

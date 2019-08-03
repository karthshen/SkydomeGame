using UnityEngine;
using System.Collections;
using InControl;

public class ActorStandingState : ActorState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        //actor.GetAnimator().enabled = true;
        ActorStandingInitialize(actor);

        PlayAnimation(actor);

        if (actor.BIsGrounded == false)
        {
            ActorJumpState state = new ActorJumpState();
            state.JumpNum = actor.JumpNum;
            return state;
        }

        if ((inputDevice.Action1 && actor.BIsGrounded))
        {
            actor.BIsGrounded = false;
            actor.Jump();
            ActorJumpState state = new ActorJumpState();

            state.JumpNum--;
            actor.JumpNum = state.JumpNum;
            state.PlayStateAnimation(actor);
            return state;
        }

        if (inputDevice.LeftStickX.Value != 0 && GetType() != typeof(ActorMovingState))
        {
            return new ActorMovingState();
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.ActorData.IdleAnimation);
    }

    private void ActorStandingInitialize(AActor actor)
    {
        if (actor.GetRigidbody().useGravity == false)
        {
            actor.GetRigidbody().useGravity = true;
        }
    }
}
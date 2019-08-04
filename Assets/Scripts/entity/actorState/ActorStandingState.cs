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
        else if (inputDevice.Action2 && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            //Debug.Log(actor.GetName() + " attacking from standing state");
            ActorState state = new ActorAttackState();
            //state.HandleInput(actor, inputDevice);
            return state;
        }
        //Ability Up input
        else if (inputDevice.Action4.WasPressed)
        {
            if (actor.CurrentEnergy >= actor.abilityUp.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                actor.abilityUp.AbilityExecute();
                return new ActorAbilityUpState();
            }
        }        
        //Ability Trigger Input
        else if ((inputDevice.RightTrigger) && (!Mathf.Approximately(inputDevice.LeftStickX.Value, 0) || !Mathf.Approximately(inputDevice.LeftStickY.Value, 0)))
        {
            if (actor.CurrentEnergy >= actor.abilityTrigger.AbilityCost)
            {
                actor.MoveHorizontal = inputDevice.LeftStickX.Value;
                actor.MoveVertical = inputDevice.LeftStickY.Value;
                actor.CastTimer = AActor.CAST_DURATION;
                actor.abilityTrigger.AbilityExecute();
                return new ActorAbilityTriggerState();
            }
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

    protected void ActorStandingInitialize(AActor actor)
    {
        if (actor.GetRigidbody().useGravity == false)
        {
            actor.GetRigidbody().useGravity = true;
        }
    }
}
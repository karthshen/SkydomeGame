using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    private int jumpNum = 2;
    private bool bAttacked = false;

    public bool BAttacked { get => bAttacked; set => bAttacked = value; }
    public int JumpNum { get => jumpNum; set => jumpNum = value; }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value != 0)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            actor.Move();
        }

        if (inputDevice.Action1.WasPressed)
        {
            if (JumpNum > 0 && actor.BIsGrounded == false)
            {
                Jumped();
                actor.JumpNum = jumpNum;
                PlayAnimation(actor);
                //Debug.Log("Jump: "+ jumpNum + "- Vertical Velocity: " + actor.GetRigidbody().velocity.y);
                actor.Jump();
                return this;
            }
        }
        else if (inputDevice.Action2 && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            //Debug.Log(actor.GetName() + " attacking from standing state");
            ActorState state = new ActorAttackState();
            //state.HandleInput(actor, inputDevice);
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
            return state;
        }
        //Ability Up input
        else if (inputDevice.Action4.WasPressed && actor.CastTimer <= 0)
        {
            if (actor.CurrentEnergy >= actor.abilityUp.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                actor.abilityUp.AbilityExecute();
                actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
                return new ActorAbilityUpState();
            }
        }
        //Ability Trigger Input
        else if ((inputDevice.RightTrigger) && (!Mathf.Approximately(inputDevice.LeftStickX.Value, 0) || !Mathf.Approximately(inputDevice.LeftStickY.Value, 0)) && actor.CastTimer <= 0)
        {
            if (actor.CurrentEnergy >= actor.abilityTrigger.AbilityCost)
            {
                actor.MoveHorizontal = inputDevice.LeftStickX.Value;
                actor.MoveVertical = inputDevice.LeftStickY.Value;
                actor.CastTimer = AActor.CAST_DURATION;
                actor.abilityTrigger.AbilityExecute();
                //actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
                return new ActorAbilityTriggerState();
            }
        }

        return this;
    }

    public void Jumped()
    {
        JumpNum--;
        hasSoundPlayed = false;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if (JumpNum == 1)
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.JumpAnimation);
            SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.JumpSound, ref hasSoundPlayed);
        }
        else if (JumpNum == 0)
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.JumpAnimation2);
            SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.JumpSound, ref hasSoundPlayed);
         }
        else
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.JumpAnimation);
            SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.JumpSound, ref hasSoundPlayed);
        }
    }
}

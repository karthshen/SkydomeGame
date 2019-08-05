using UnityEngine;
using System.Collections;
using InControl;

public class ActorMovingState : ActorStandingState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value == 0)
        {
            return new ActorStandingState();
        }
        else
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            actor.Move();
        }

        return base.HandleInput(actor, inputDevice);
    }

    protected override void PlayAnimation(AActor actor)
    {  
        if (Mathf.Abs(actor.MoveHorizontal) < 0.6f)
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.WalkAnimation);
            SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.WalkingSound, ref hasSoundPlayed);
        }
        else
        {
            actor.GetAnimatorController().SetInt(actor.ActorData.RunningAnimation);
            SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.RunningSound, ref hasSoundPlayed);
        }
    }
}

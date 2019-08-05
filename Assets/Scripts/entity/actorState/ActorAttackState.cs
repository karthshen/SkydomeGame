using UnityEngine;
using System.Collections;
using InControl;

public class ActorAttackState : ActorState
{
    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.Action2 && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            PlayAnimation(actor);
            //Debug.Log("Attack Timer for " + actor.GetName() + " is " + actor.AttackTimer);
            actor.Attack();
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.ActorData.AttackAnimation1);
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.AttackSound1, ref hasSoundPlayed);
    }
}

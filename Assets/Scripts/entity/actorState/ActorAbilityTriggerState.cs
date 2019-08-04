using UnityEngine;
using System.Collections;

public class ActorAbilityTriggerState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.ActorData.AbilityTriggerAnimation);
        //SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.GetActorStat().AbilityTriggerSound, ref hasSoundPlayed);

        if (!actor.BIsGrounded && actor.abilityTrigger.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
        }

        base.PlayAnimation(actor);
    }
}

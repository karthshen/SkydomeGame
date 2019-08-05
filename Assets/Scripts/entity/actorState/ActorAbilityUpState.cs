using UnityEngine;
using System.Collections;

public class ActorAbilityUpState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.ActorData.AbilityUpAnimation);
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.ActorData.AbilityUpSound, ref hasSoundPlayed);

        if (!actor.BIsGrounded && actor.abilityUp.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
        }

        base.PlayAnimation(actor);
    }
}

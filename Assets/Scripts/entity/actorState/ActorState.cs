using UnityEngine;
using System.Collections;
using InControl;

public abstract class ActorState
{
    public bool hasSoundPlayed = false;

    protected ActorState()
    {
        Debug.Log("Entering State: " + GetType());
    }

    protected ActorState(string previousState)
    {
        Debug.Log("Previous State: " + previousState + " transit to Current State: " + GetType());
    }

    public abstract ActorState HandleInput(AActor actor, InputDevice inputDevice);

    protected abstract void PlayAnimation(AActor actor);

    public void PlayStateAnimation(AActor actor)
    {
        PlayAnimation(actor);
    }
}

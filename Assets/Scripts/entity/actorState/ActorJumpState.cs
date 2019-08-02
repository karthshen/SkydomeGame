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

        return this;
    }

    public void Jumped()
    {
        JumpNum--;
        hasSoundPlayed = false;
    }

    protected override void PlayAnimation(AActor actor)
    {
        //throw new System.NotImplementedException();
    }
}

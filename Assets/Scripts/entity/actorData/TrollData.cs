using UnityEngine;
using System.Collections;

public class TrollData : ActorData
{
    public TrollData()
    {
        MaxHealth = 100;
        MaxEnergy = 100;
        MoveVelocity = 3;
        JumpVelocity = 5;
        SprintVelocity = 100;
        AttackPower = 15;
        DefensePower = 15;

        //animation
        AttackAnimation1 = "animation,23";
        AttackAnimation2 = "animation,23";
        AttackAnimation3 = "animation,23";

        AbilityDownAnimation = "animation,23";
        AbilityHorizAnimation = "animation,32";
        AbilityUpAnimation = "animation,23";
        AbilityTriggerAnimation = "animation,32";
        AbilityBumperAnimation = "animation,23";
        AbilityNeutralAnimation = "animation,32";

        WalkAnimation = "animation,17";
        RunningAnimation = "animation,20";

        JumpAnimation = "animation,16";
        JumpAnimation2 = "animation,4";

        FreezeAnimation = "animation,8";

        DeathAnimation = "animation,10";
        IdleAnimation = "animation,13";
    }
}

using UnityEngine;
using System.Collections;

public class BullBossData : ActorData
{
    public BullBossData()
    {
        MaxHealth = 200;
        MaxEnergy = 100;
        MoveVelocity = 4.1f;
        JumpVelocity = 5;
        SprintVelocity = 100;
        AttackPower = 25;
        DefensePower = 15;

        //animation
        AttackAnimation1 = "animation,23";
        AttackAnimation2 = "animation,24";
        AttackAnimation3 = "animation,25";

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

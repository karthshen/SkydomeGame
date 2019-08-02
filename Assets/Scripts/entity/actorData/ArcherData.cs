using UnityEngine;
using System.Collections;

public class ArcherData : ActorData
{
    public ArcherData()
    {
        MaxHealth = 100;
        MaxEnergy = 100;
        MoveVelocity = 7;
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

        DeathAnimation = "animation,10";
        IdleAnimation = "animation,13";
}
}

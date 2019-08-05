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

        WalkAnimation = "animation,17";
        RunningAnimation = "animation,20";

        JumpAnimation = "animation,16";
        JumpAnimation2 = "animation,4";

        FreezeAnimation = "animation,8";

        DeathAnimation = "animation,10";
        IdleAnimation = "animation,13";

        //sound
        AttackSound1 = SoundManager.instance.arrow_attack1;
        AttackSound2 = SoundManager.instance.arrow_attack2;
        AttackSound3 = SoundManager.instance.arrow_attack2;

        AbilityNeutralSound = SoundManager.instance.chicken1;
        AbilityHorizSound = SoundManager.instance.trap;
        AbilityDownSound = SoundManager.instance.arrow_attack2;
        AbilityTriggerSound = SoundManager.instance.hookshoot;
        AbilityUpSound = SoundManager.instance.arrow_attack2;
    }
}

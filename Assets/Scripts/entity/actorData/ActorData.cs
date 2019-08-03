using UnityEngine;
using System.Collections;

public abstract class ActorData
{
    private float maxHealth;
    private float maxEnergy;
    private float moveVelocity;
    private float jumpVelocity;
    private float sprintVelocity;
    private float attackPower;
    private float defensePower;
    private float abiltiyPower;

    //Animation
    private string attackAnimation1;
    private string attackAnimation2;
    private string attackAnimation3;
    private string movingAnimation;
    private string walkAnimation;
    private string runningAnimation;
    private string freezeAnimation;
    private string abilityDownAnimation;
    private string abilityHorizAnimation;
    private string abilityUpAnimation;
    private string abilityTriggerAnimation;
    private string abilityBumperAnimation;
    private string abilityNeutralAnimation;
    private string jumpAnimation;
    private string jumpAnimation2;
    private string deathAnimation;
    private string idleAnimation;

    //Sound FX
    private AudioClip attackSound1;
    private AudioClip attackSound2;
    private AudioClip attackSound3;
    private AudioClip movingSound;
    private AudioClip walkingSound;
    private AudioClip runningSound;
    private AudioClip damagedSound;
    private AudioClip abilityDownSound;
    private AudioClip abilityUpSound;
    private AudioClip abilityHorizSound;
    private AudioClip abilityNeutralSound;
    private AudioClip abilityTriggerSound;
    private AudioClip jumpSound;
    private AudioClip landSound;
    private AudioClip deathSound;
    private AudioClip idleSound;


    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
    public float MoveVelocity { get => moveVelocity; set => moveVelocity = value; }
    public float JumpVelocity { get => jumpVelocity; set => jumpVelocity = value; }
    public float SprintVelocity { get => sprintVelocity; set => sprintVelocity = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }
    public float DefensePower { get => defensePower; set => defensePower = value; }
    public float AbiltiyPower { get => abiltiyPower; set => abiltiyPower = value; }
    public string AttackAnimation1 { get => attackAnimation1; set => attackAnimation1 = value; }
    public string AttackAnimation2 { get => attackAnimation2; set => attackAnimation2 = value; }
    public string AttackAnimation3 { get => attackAnimation3; set => attackAnimation3 = value; }
    public string MovingAnimation { get => movingAnimation; set => movingAnimation = value; }
    public string WalkAnimation { get => walkAnimation; set => walkAnimation = value; }
    public string RunningAnimation { get => runningAnimation; set => runningAnimation = value; }
    public string FreezeAnimation { get => freezeAnimation; set => freezeAnimation = value; }
    public string AbilityDownAnimation { get => abilityDownAnimation; set => abilityDownAnimation = value; }
    public string AbilityHorizAnimation { get => abilityHorizAnimation; set => abilityHorizAnimation = value; }
    public string AbilityUpAnimation { get => abilityUpAnimation; set => abilityUpAnimation = value; }
    public string AbilityTriggerAnimation { get => abilityTriggerAnimation; set => abilityTriggerAnimation = value; }
    public string AbilityBumperAnimation { get => abilityBumperAnimation; set => abilityBumperAnimation = value; }
    public string AbilityNeutralAnimation { get => abilityNeutralAnimation; set => abilityNeutralAnimation = value; }
    public string JumpAnimation { get => jumpAnimation; set => jumpAnimation = value; }
    public string JumpAnimation2 { get => jumpAnimation2; set => jumpAnimation2 = value; }
    public string DeathAnimation { get => deathAnimation; set => deathAnimation = value; }
    public string IdleAnimation { get => idleAnimation; set => idleAnimation = value; }
    public AudioClip AttackSound1 { get => attackSound1; set => attackSound1 = value; }
    public AudioClip AttackSound2 { get => attackSound2; set => attackSound2 = value; }
    public AudioClip AttackSound3 { get => attackSound3; set => attackSound3 = value; }
    public AudioClip MovingSound { get => movingSound; set => movingSound = value; }
    public AudioClip WalkingSound { get => walkingSound; set => walkingSound = value; }
    public AudioClip RunningSound { get => runningSound; set => runningSound = value; }
    public AudioClip DamagedSound { get => damagedSound; set => damagedSound = value; }
    public AudioClip AbilityDownSound { get => abilityDownSound; set => abilityDownSound = value; }
    public AudioClip AbilityUpSound { get => abilityUpSound; set => abilityUpSound = value; }
    public AudioClip AbilityHorizSound { get => abilityHorizSound; set => abilityHorizSound = value; }
    public AudioClip AbilityNeutralSound { get => abilityNeutralSound; set => abilityNeutralSound = value; }
    public AudioClip AbilityTriggerSound { get => abilityTriggerSound; set => abilityTriggerSound = value; }
    public AudioClip JumpSound { get => jumpSound; set => jumpSound = value; }
    public AudioClip LandSound { get => landSound; set => landSound = value; }
    public AudioClip DeathSound { get => deathSound; set => deathSound = value; }
    public AudioClip IdleSound { get => idleSound; set => idleSound = value; }
}
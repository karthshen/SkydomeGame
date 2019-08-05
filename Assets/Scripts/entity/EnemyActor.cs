using UnityEngine;
using System.Collections;

public abstract class EnemyActor : AActor
{
    protected bool engagedCombat = false;
    protected CameraController skydomeCamera;
    private AActor player;
    public float ATTACK_RANGE = 3f;

    public AActor Player { get => player; set => player = value; }

    public bool IsEngagedInCombat()
    {
        return engagedCombat;
    }

    public bool IsAttackInRange()
    {
        return Vector3.Distance(player.transform.position, this.transform.position) < ATTACK_RANGE;
    }
}

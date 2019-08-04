using UnityEngine;
using System.Collections;

public abstract class EnemyActor : AActor
{
    protected bool engagedCombat = false;
    protected CameraController skydomeCamera;

    public bool IsEngagedInCombat()
    {
        return engagedCombat;
    }
}

using UnityEngine;
using System.Collections;

public abstract class ProjectileItem : Item
{
    public abstract void ProjectileStart();

    public abstract void ProjectileFinish();
}

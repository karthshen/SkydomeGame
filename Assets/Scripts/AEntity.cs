using UnityEngine;
using System.Collections;

public abstract class AEntity : MonoBehaviour
{
    protected void IgnoreOwnerCollision(AActor owner)
    {
        if (!owner)
            return;

        Collider[] collidersToIgnore = owner.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach (Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = owner.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        foreach (Collider selfCollider in colliderInSelf)
        {
            Physics.IgnoreCollision(selfCollider, ownerCollider);
        }
    }

    protected void SetRotationToEntity(AEntity entity)
    {
        gameObject.transform.GetChild(0).rotation = entity.transform.GetChild(0).rotation;
    }

    public float GetYDirectionInRadian()
    {
        return transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;
    }

    protected void IgnoreGameobjectCollision(GameObject gameObject)
    {
        if (!gameObject)
            return;

        Collider[] collidersToIgnore = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach (Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = gameObject.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        if (ownerCollider)
        {
            foreach (Collider selfCollider in colliderInSelf)
            {
                Physics.IgnoreCollision(selfCollider, ownerCollider);
            }
        }
    }

    public void SetPositionToEnitty(AEntity entity)
    {
        gameObject.transform.position = entity.transform.position;
    }
}

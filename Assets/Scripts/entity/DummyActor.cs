using UnityEngine;
using System.Collections;

public class DummyActor : AActor
{
    public AActor player;
    public int positionX = 10;
    public int positionY = 2;
    public int positioNZ = 0;

    private Vector3 playerCurrentDirection;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        playerCurrentDirection = player.transform.GetChild(0).eulerAngles;
        SetPositionToPlayer(positionX, positionY, positioNZ);
    }

    private void Update()
    {
        if (player.transform.GetChild(0).eulerAngles == frontDirection)
        {
            SetPositionToPlayer(positionX, 0, 0);
        } else if (player.transform.GetChild(0).eulerAngles == backDirection)
        {
            SetPositionToPlayer(-positionX, 0, 0);
        }
    }

    private void SetPositionToPlayer(int diffX, int diffY, int diffZ)
    {
        playerCurrentDirection = player.transform.GetChild(0).eulerAngles;
        this.transform.position = new Vector3
            (player.transform.position.x + diffX, player.transform.position.y + diffY, player.transform.position.z + diffZ);

    }
}

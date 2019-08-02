using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputHandler : MonoBehaviour
{
    public AActor player;
    private int playerNum = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        
        if(inputDevice != null)
        {
            HandleInput(inputDevice);
        }
    }

    private void HandleInput(InputDevice inputDevice)
    {
        AActor actor = player;

        if(!actor)
        {
            Debug.Log("Actor not spawned for player");
        }

        if (actor.IsActorAlive())
        {
            actor.HandleInput(inputDevice);
        }
    }
}

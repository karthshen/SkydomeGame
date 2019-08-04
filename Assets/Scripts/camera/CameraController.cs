using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float CAMERA_SIZE_FACTOR = 1.5f;

    [SerializeField]
    private float DEFAULT_CAMERA_DISTANCE = 10f;
    [SerializeField]
    private float COMBAT_CAMERA_DISTANCE = 3f;

    Camera skydomeCamera;

    float minX;
    float maxX;
    float minY;
    float maxY;

    Vector3 desiredPos;

    float arenaCameraeraDistance;

    public List<AActor> actors;

    private AActor player;

    private AActor dummy;

    private float CONSTANT_CLOSE;

    [SerializeField]
    private float cameraSpeed = 1f;

    public float CameraSpeed
    {
        get
        {
            return cameraSpeed;
        }

        set
        {
            cameraSpeed = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        skydomeCamera = GetComponent<Camera>();
        arenaCameraeraDistance = transform.position.z;

        CONSTANT_CLOSE = DEFAULT_CAMERA_DISTANCE;

        //Temp for constants
        //Physics.gravity = new Vector3(Physics.gravity.x, -9.81f * 1.54f * 1.54f, Physics.gravity.z);

        Physics.IgnoreLayerCollision(8, 9, true);

        Physics.IgnoreLayerCollision(8, 8, true);

        player = actors[0];
        dummy = actors[1];

        if(!player || !dummy)
        {
            throw new UnassignedReferenceException();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (actors.Count < 2)
        {
            FindDefaultActors();
        }

        CalculateCameraBounds();
        UpdateCameraPosition();
        MoveCamera();
    }

    private void FindDefaultActors()
    {
        actors.Clear();
        actors.Add(player);
        actors.Add(dummy);

        CONSTANT_CLOSE = DEFAULT_CAMERA_DISTANCE;
    }

    public void ActorSpottedInCamera(AActor actor)
    {
        actors.RemoveAt(1);
        actors.Add(actor);

        CONSTANT_CLOSE = COMBAT_CAMERA_DISTANCE;
    } 

    private void MoveCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, CameraSpeed);
    }

    private void UpdateCameraPosition()
    {
        var CONSTANT_MULTIPLIER = 0.3f;

        if (actors.Count <= 0)//early out if no actors have been found
            return;
        desiredPos = new Vector3(0, 3, 0);
        float distance = 0f;
        var mHeight = maxY - minY;
        var mWidth = maxX - minX;
        var distanceH = -(mHeight + CONSTANT_CLOSE) * CONSTANT_MULTIPLIER / Mathf.Tan(skydomeCamera.fieldOfView * CONSTANT_MULTIPLIER * Mathf.Deg2Rad);
        var distanceW = -(mWidth / skydomeCamera.aspect + CONSTANT_CLOSE) * CONSTANT_MULTIPLIER / Mathf.Tan(skydomeCamera.fieldOfView * CONSTANT_MULTIPLIER * Mathf.Deg2Rad);
        distance = distanceH < distanceW ? distanceH : distanceW;

        for (int i = 0; i < actors.Count; i++)
        {
            desiredPos += actors[i].transform.position;
        }
        if (distance > -CONSTANT_CLOSE) distance = -CONSTANT_CLOSE;
        //Debug.Log("Distance: "+distance);
        desiredPos /= actors.Count;
        desiredPos.z = distance;
    }

    private void CalculateCameraBounds()
    {
        minX = Mathf.Infinity;
        maxX = -Mathf.Infinity;

        minY = Mathf.Infinity;
        maxY = -Mathf.Infinity;

        foreach (AActor actor in actors)
        {
            Vector3 tempActor = actor.transform.position;

            float tempX = tempActor.x + CAMERA_SIZE_FACTOR;
            float tempY = tempActor.y + CAMERA_SIZE_FACTOR;

            minX = tempActor.x < minX ? tempActor.x : minX;
            //maxX = tempActor.x > maxX ? tempActor.x : maxX;

            minY = tempActor.y < minY ? tempActor.y : minY;
            //maxY = tempActor.y > maxY ? tempActor.y : maxY;

            //minX = tempX < minX ? tempX : minX;
            maxX = tempX > maxX ? tempX : maxX;

            //minY = tempY < minY ? tempY : minY;
            maxY = tempY > maxY ? tempY : maxY;
        }

        //maxX *= 1.5f;
        //maxY *= 1.5f;
    }

    private void CalculateCmaeraPosAndSize()
    {
        Vector3 arenaCameraeraCenter = Vector3.zero;
        Vector3 finalLookat = Vector3.zero;

        foreach (AActor actor in actors)
        {
            arenaCameraeraCenter += actor.transform.position;
        }

        arenaCameraeraCenter = arenaCameraeraCenter / actors.Count;

        //Positions arenaCameraera around a center point
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arenaCameraeraCenter;

        transform.position = Vector3.Lerp(transform.position, position, CameraSpeed * Time.deltaTime);

        finalLookat = Vector3.Lerp(finalLookat, arenaCameraeraCenter, CameraSpeed * Time.deltaTime);
        //Look at
        transform.LookAt(finalLookat);
    }
}

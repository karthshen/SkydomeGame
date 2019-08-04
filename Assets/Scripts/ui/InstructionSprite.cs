using UnityEngine;
using System.Collections;

public class InstructionSprite : MonoBehaviour
{
    private AActor player;
    private bool displayed = false;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            displayed = true;
        }
        else
        {
            if (displayed)
                Destroy(gameObject);
        }
    }
}

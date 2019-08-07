using UnityEngine;
using System.Collections;

public class Portal : Item
{
    public Portal endPoint;

    AudioSource audioSource;

    private void Start()
    {
        ParticleSystem[] pss = gameObject.GetComponentsInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        foreach (ParticleSystem ps in pss)
        {
            ps.playbackSpeed = 2f;
        }
    }

    public override void UseItem(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        ArcherActor actor = other.gameObject.GetComponent<ArcherActor>();

        if(actor)
        {
            actor.transform.position = endPoint.transform.position;
            actor.transform.position += new Vector3(2, 0, 0);
            bool hasPlayed = false;
            SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.teleport, ref hasPlayed);
        }
    }
}

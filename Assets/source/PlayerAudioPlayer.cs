using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PickUp))]
[RequireComponent(typeof(Throw))]
public class PlayerAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip pickupAudio = null;
    [SerializeField] AudioClip dropAudio = null;
    [SerializeField] AudioClip tossAudio = null;

    Throw throwComp = null;
    PickUp pickupComp = null;
    AudioSource sourceComp = null;
    // Start is called before the first frame update
    void Start()
    {
        pickupComp = GetComponent<PickUp>();
        throwComp = GetComponent<Throw>();
        sourceComp = GetComponent<AudioSource>();

        pickupComp.pickupEvent.AddListener(PlayPickupAudio);
        throwComp.dropEvent.AddListener(PlayDropAudio);
        throwComp.tossEvent.AddListener(PlayTossAudio);
    }

    void PlayPickupAudio() { PlayAudio(pickupAudio); }
    void PlayDropAudio() { PlayAudio(dropAudio); }
    void PlayTossAudio() { PlayAudio(tossAudio); }

    void PlayAudio(AudioClip audio)
    {
        if (audio != null)
        {
            sourceComp.PlayOneShot(audio);
        }
    }
}

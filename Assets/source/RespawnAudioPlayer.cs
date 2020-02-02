using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(RespawnTrigger))]
public class RespawnAudioPlayer : MonoBehaviour
{
    AudioSource source = null;
    RespawnTrigger respawnTrigger = null;

    void Start()
    {
        source = GetComponent<AudioSource>();
        respawnTrigger = GetComponent<RespawnTrigger>();
        respawnTrigger.triggerEvent.AddListener(Play);
    }

    // Update is called once per frame
    void Play()
    {
        source.Play();
    }
}

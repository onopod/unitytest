using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SEPlayer: MonoBehaviour
{
    protected AudioSource source;

    [SerializeField] AudioClip footstep;
    [SerializeField] AudioClip spawn;
    [SerializeField] AudioClip charge;
    [SerializeField] AudioClip despawn;

    private void Awake()
    {
        source = GetComponents<AudioSource>()[0];
    }

    public void PlayFootstepSE()
    {
        float pitchRange = 0.1f;
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(footstep);
    }
    public void PlaySpawnSE() => source.PlayOneShot(spawn);
    public void PlayChargeSE() => source.PlayOneShot(charge);
    public void PlayDespawnSE() => source.PlayOneShot(despawn);

}
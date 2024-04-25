using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;
    
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioClip[] audioClips;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayMySoundtrack();
    }

    private void PlayMySoundtrack()
    {
        audioSources[0].clip = audioClips[0];
        audioSources[0].Play();
        audioSources[0].loop = true;
    }
}

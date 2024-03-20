using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Sources -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("----- Audio Clips -----")]
    public AudioClip background;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider ballVolumeSlider;
    
    public void SetMasterVolume()
    {
        float volume = masterVolumeSlider.value;
        if (volume <= 0.01f) volume = 0.01f;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        if (volume <= 0.01f) volume = 0.01f;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void SetSfxVolume()
    {
        float volume = sfxVolumeSlider.value;
        if (volume <= 0.01f) volume = 0.01f;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    
    public void SetBallVolume()
    {
        float volume = ballVolumeSlider.value;
        if (volume <= 0.01f) volume = 0.01f;
        audioMixer.SetFloat("Rolling", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BallVolume", volume);
    }
    
    private void LoadMusicVolume()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumeSlider.value = volume;
        SetMusicVolume();
    }
    
    private void LoadSfxVolume()
    {
        float volume = PlayerPrefs.GetFloat("SFXVolume");
        sfxVolumeSlider.value = volume;
        SetSfxVolume();
    }
    
    private void LoadBallVolume()
    {
        float volume = PlayerPrefs.GetFloat("BallVolume");
        ballVolumeSlider.value = volume;
        SetBallVolume();
    }
    
    private void LoadMasterVolume()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume");
        masterVolumeSlider.value = volume;
        SetMasterVolume();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume")) LoadMusicVolume();
        else SetMusicVolume();
        
        if (PlayerPrefs.HasKey("sfxVolume")) LoadSfxVolume();
        else SetSfxVolume();
        
        if (PlayerPrefs.HasKey("ballVolume")) LoadBallVolume();
        else SetBallVolume();
        
        if (PlayerPrefs.HasKey("masterVolume")) LoadMasterVolume();
        else SetMasterVolume();
    }
}

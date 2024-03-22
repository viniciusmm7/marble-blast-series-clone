using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Sources -----")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioSource rollingSource;
    
    [Header("----- Audio Clips -----")]
    public AudioClip background;
	public AudioClip buttonClick;
	public AudioClip playerJump;
	public AudioClip gemCollected;
	public AudioClip allGemsCollected;
	public AudioClip finishLevel;
	public AudioClip helpTrigger;
	public AudioClip missingGems;
	public AudioClip outOfBounds;
	public AudioClip rolling;
	
    public void PlaySfx(AudioClip clip, bool isLooping = false)
	{
		if (clip == rolling)
		{
			if (rollingSource.isPlaying) return;
			rollingSource.clip = clip;
			rollingSource.loop = isLooping;
			rollingSource.Play();
			return;
		}
		
		sfxSource.PlayOneShot(clip);
	}

	public void StopRollingSfx()
	{
		rollingSource.Stop();
	}
    
    private void Awake()
    {
		DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}

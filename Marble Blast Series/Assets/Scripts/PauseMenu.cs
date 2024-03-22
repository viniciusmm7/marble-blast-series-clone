using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject settingsCanvas;

    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Pause(bool clicked = true)
    {
        if (clicked) audioManager.PlaySfx(audioManager.buttonClick);
        pauseCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void Continue(bool clicked = true)
    {
        if (clicked) audioManager.PlaySfx(audioManager.buttonClick);
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Settings()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        settingsCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
    
    public void Quit()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }
    
    private void Update()
    {
        if (!Input.GetButtonDown("Cancel")) return;
        if (pauseCanvas.activeSelf)
        {
            Continue(false);
        }
        else
        {
            Pause(false);
        }
    }
}

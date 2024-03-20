using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject settingsCanvas;

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void Continue()
    {
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Settings()
    {
        settingsCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
    
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseCanvas.activeSelf)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
}

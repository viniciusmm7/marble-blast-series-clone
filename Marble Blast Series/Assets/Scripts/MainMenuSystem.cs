using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void PauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}

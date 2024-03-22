using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void PlayGame()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void MainMenu()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Settings()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        Application.Quit();
    }
    
    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}

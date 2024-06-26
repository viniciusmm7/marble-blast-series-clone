using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuSystem : MonoBehaviour
{
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void NextLevel()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MainMenu()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(0);
    }
}

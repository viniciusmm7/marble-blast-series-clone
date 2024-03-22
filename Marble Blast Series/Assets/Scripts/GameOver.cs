using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Retry()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        audioManager.PlaySfx(audioManager.buttonClick);
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }
}

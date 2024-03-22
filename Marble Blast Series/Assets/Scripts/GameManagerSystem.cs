using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerSystem : MonoBehaviour
{
    private static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Restart Level"))
        {
            RestartScene();
        }
    }
}

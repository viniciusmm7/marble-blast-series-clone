using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerSystem : MonoBehaviour
{
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Restart Level"))
        {
            RestartScene();
        }
    }
}

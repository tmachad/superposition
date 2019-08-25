using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string m_MainMenuSceneName;
    public string[] m_LevelSceneNames;

    public void Pause()
    {
        // Pause game and open pause menu
    }

    public void Resume()
    {
        // Resume game and close pause menu
    }

    public void MainMenu()
    {
        // Go to main menu scene
    }

    public void LoadLevel(int index)
    {
        // Load level from level array
        SceneManager.LoadScene(m_LevelSceneNames[index], LoadSceneMode.Single);
    }
}

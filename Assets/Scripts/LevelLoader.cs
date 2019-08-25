using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    public string m_MainMenuSceneName;
    public string[] m_LevelSceneNames;

    private void Awake()
    {
        Instance = this;
    }

    public void MainMenu()
    {
        // Go to main menu scene
        SceneManager.LoadScene(m_MainMenuSceneName, LoadSceneMode.Single);
    }

    public void LoadLevel(int index)
    {
        // Load level from level array
        SceneManager.LoadScene(m_LevelSceneNames[index], LoadSceneMode.Single);
    }
}

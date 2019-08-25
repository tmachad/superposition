using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject m_LoseMenu;
    public GameObject m_WinMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        m_LoseMenu.SetActive(true);
        PauseManager.Instance.Pause(false);
    }

    public void Win()
    {
        m_WinMenu.SetActive(true);
        PauseManager.Instance.Pause(false);
    }

    public void Quit()
    {
        // Quit the game
        Application.Quit();
    }
}

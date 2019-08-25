using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject m_PauseMenuPanel;
    public KeyCode m_TogglePauseKey;

    private bool m_Paused = false;

    private void Start()
    {
        m_PauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_TogglePauseKey))
        {
            if (m_Paused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    private void OnDestroy()
    {
        if (m_Paused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        m_PauseMenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
        m_Paused = true;
    }

    public void Resume()
    {
        m_PauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
        m_Paused = false;
    }
}

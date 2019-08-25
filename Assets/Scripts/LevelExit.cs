using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public GameObject m_WinMenu;

    public LayerMask m_PlayerLayer;

    private void Start()
    {
        m_WinMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_PlayerLayer == (m_PlayerLayer | 1 << other.gameObject.layer))
        {
            PauseManager.Instance.Pause(false);
            m_WinMenu.SetActive(true);
        }
    }
}

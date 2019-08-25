using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public LayerMask m_PlayerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_PlayerLayer == (m_PlayerLayer | 1 << other.gameObject.layer))
        {
            GameManager.Instance.Win();
        }
    }
}

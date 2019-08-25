using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelDimension : MonoBehaviour
{
    public Rigidbody2D m_Character;
    public KeyCode m_SwapHereKey;
    public PostProcessLayer m_CameraEffect;
    public Rect m_KillBounds;

    private void Start()
    {
        m_CameraEffect.enabled = m_Character.GetComponent<PlayerController>() == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_KillBounds.position, m_KillBounds.size);
    }

    public void SwapCharacters(LevelDimension other)
    {
        // Swap character positions
        Vector2 otherPosition = other.m_Character.transform.position;
        other.m_Character.transform.position = m_Character.transform.position;
        m_Character.transform.position = otherPosition;

        // Swap character transform parents
        other.m_Character.transform.parent = transform;
        m_Character.transform.parent = other.transform;

        // Toggle camera effects on/off if new character is the player
        other.m_CameraEffect.enabled = m_Character.GetComponent<PlayerController>() == null;
        m_CameraEffect.enabled = other.m_Character.GetComponent<PlayerController>() == null;

        // Swap character references
        Rigidbody2D otherCharacter = other.m_Character;
        other.m_Character = m_Character;
        m_Character = otherCharacter;
    }

    public bool ContainsCharacter()
    {
        return m_KillBounds.Contains(m_Character.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelDimension : MonoBehaviour
{
    public Rigidbody2D m_Character;
    public KeyCode m_SwapHereKey;
    public Camera m_Camera;
    public Rect m_KillBounds;
    public LayerMask m_GroundLayer;

    private PostProcessLayer m_CameraEffect;

    private void Start()
    {
        m_CameraEffect = m_Camera.GetComponent<PostProcessLayer>();
        m_CameraEffect.enabled = m_Character.GetComponent<PlayerController>() == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + m_KillBounds.center, m_KillBounds.size);
    }

    public bool SwapCharacters(LevelDimension other)
    {
        BoxCollider2D collider = m_Character.GetComponent<BoxCollider2D>();
        Collider2D overlap = Physics2D.OverlapBox(m_Character.position, collider.size * 0.95f, 0, m_GroundLayer);
        BoxCollider2D otherCollider = other.m_Character.GetComponent<BoxCollider2D>();
        Collider2D otherOverlap = Physics2D.OverlapBox(other.m_Character.position, otherCollider.size * 0.95f, 0, m_GroundLayer);

        if (overlap != null || otherOverlap != null)
        {
            // One of the two characters is inside a wall, so don't swap
            return false;
        }

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

        return true;
    }

    public bool ContainsCharacter()
    {
        return m_KillBounds.Contains(m_Character.transform.localPosition);
    }
}

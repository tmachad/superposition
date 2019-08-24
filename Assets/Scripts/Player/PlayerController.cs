using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float m_GroundSpeed = 3.0f;
    public float m_AirDrag = 0.1f;
    public float m_JumpHeight = 3.0f;

    [SerializeField]
    private LayerMask m_GroundLayer;
    [SerializeField]
    private float m_GroundCheckDistance = 0.05f;

    private Rigidbody2D m_Rigidbody;
    private Collider2D m_Collider;
    private bool m_Grounded = false;
    private float m_JumpSpeed;
    private bool m_JumpStillPressed = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        m_JumpSpeed = Mathf.Sqrt(-2 * Physics2D.gravity.y * m_JumpHeight);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        Vector2 velocity = new Vector2(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y);

        // Check for ground below the player
        Vector2 bottom = new Vector2(transform.position.x + m_Collider.offset.x, transform.position.y + m_Collider.offset.y - m_Collider.bounds.extents.y);
        Vector2 boxSize = new Vector2(m_Collider.bounds.extents.x * 2 * 0.95f, m_GroundCheckDistance);
        Collider2D collider = Physics2D.OverlapBox(bottom, boxSize, 0, m_GroundLayer);
        m_Grounded = collider != null;

        // Move horizontally if player is pressing a button, otherwise just maintain current horizontal velocity
        if (horizontal != 0)
        {
            velocity.x = horizontal * m_GroundSpeed;
        } else if (!m_Grounded)
        {
            // When not giving input and not in the air apply air drag so the player doesn't sideways coast forever
            velocity.x = velocity.x * (1 - m_AirDrag);
        }

        // Jump if grounded
        if (vertical != 0 && m_Grounded && !m_JumpStillPressed)
        {
            velocity.y = vertical * m_JumpSpeed;
            m_JumpStillPressed = true;
        } else if (vertical == 0)
        {
            m_JumpStillPressed = false;
        }
        
        m_Rigidbody.velocity = velocity;
    }
}

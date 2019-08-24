using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SuperpositionController : MonoBehaviour
{

    public GameObject m_Follow;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 followRelativePos = m_Follow.transform.position;

        m_Rigidbody.MovePosition(transform.parent.position + followRelativePos);
    }
}

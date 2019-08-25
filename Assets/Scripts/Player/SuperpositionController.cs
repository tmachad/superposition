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
        m_Follow.GetComponent<PlayerController>().m_AnimationSyncEvent.AddListener(SyncAnimations);
    }

    private void FixedUpdate()
    {
        Vector3 relativeFollowPos = m_Follow.transform.position - m_Follow.transform.parent.position;

        m_Rigidbody.MovePosition(transform.parent.position + relativeFollowPos);
    }

    private void SyncAnimations(AnimationSyncData syncData)
    {
        if (syncData.Jump)
        {
            m_Animator.SetTrigger("Jump");
        }
        if (syncData.HitGround)
        {
            m_Animator.SetTrigger("Hit Ground");
        }
        m_Animator.SetBool("Walking", syncData.Walking);
        m_Animator.SetFloat("Vertical Speed", syncData.Velocity.y);

        if (syncData.Input.x < 0)
        {
            // Flip sprite to face left when moving left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        } else if (syncData.Input.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    } 
}

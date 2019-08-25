using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject m_LoseMenu;
    public LevelDimension[] m_Dimensions;

    private LevelDimension m_ActiveDimension;

    private void Start()
    {
        foreach (LevelDimension dimension in m_Dimensions)
        {
            if (dimension.m_Character.GetComponent<PlayerController>() != null)
            {
                m_ActiveDimension = dimension;
                break;
            }
        }
    }

    private void Update()
    {
        foreach (LevelDimension dimension in m_Dimensions)
        {
            if (Input.GetKeyDown(dimension.m_SwapHereKey) && dimension != m_ActiveDimension)
            {
                m_ActiveDimension.SwapCharacters(dimension);
                m_ActiveDimension = dimension;
                break;
            }
        }

        if (!m_ActiveDimension.ContainsCharacter())
        {
            // Player has left playable area; game over
            PauseManager.Instance.Pause(false);
            m_LoseMenu.SetActive(true);
        }
    }
}

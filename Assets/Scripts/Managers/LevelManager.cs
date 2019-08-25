﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public LevelDimension[] m_Dimensions;

    private LevelDimension m_ActiveDimension;

    void Start()
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

    void Update()
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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper {
    /// <summary>
    /// Ranges from 0.0 to 1.0f
    /// </summary>
    float m_leftBorder = 0.0f;
    float m_rightBorder = 0.0f;
    float m_buffering = 0.05f;

    public ScreenWrapper()
    {
        m_leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 10.0f)).x;
        m_rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 10.0f)).x;
    }

    public Vector3 Wrap(Vector3 target)
    {
        if (target.x < m_leftBorder + m_buffering || target.x > m_rightBorder + m_buffering)
        {
            target.x *= -1;
        }

        return target;
    }

}

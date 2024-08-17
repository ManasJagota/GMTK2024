using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverButton : MonoBehaviour
{
    [SerializeField] ELeverButtonType m_LeverButtonType;

    [SerializeField] GameObject m_GOLeverActionToPerformOn;

    private void OnCollisionEnter2D(Collision2D a_other)
    {
        if (a_other.transform.CompareTag("Player"))
        {
            if (m_LeverButtonType.Equals(ELeverButtonType.Enable))
                m_GOLeverActionToPerformOn.SetActive(true);
            else if (m_LeverButtonType.Equals(ELeverButtonType.Disable))
                m_GOLeverActionToPerformOn.SetActive(false);
            else if(m_LeverButtonType.Equals(ELeverButtonType.PlatformMove))
                m_GOLeverActionToPerformOn.GetComponent<WaypointMover>().enabled = true;
        }
    }
}

public enum ELeverButtonType
{
    Enable = 0,
    Disable = 1,
    PlatformMove = 2,
}

using Com.LuisPedroFonseca.ProCamera2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverButton : MonoBehaviour
{
    [SerializeField] ELeverButtonType m_LeverButtonType;

    [SerializeField] GameObject m_GOLeverActionToPerformOn;
    private int m_count = 0;

    private void OnCollisionEnter2D(Collision2D a_other)
    {
        if (a_other.transform.CompareTag("Player"))
        {
            if (m_count == 0)
            {
                m_count = 1;
                if (m_LeverButtonType.Equals(ELeverButtonType.Enable))
                    m_GOLeverActionToPerformOn.SetActive(true);
                else if (m_LeverButtonType.Equals(ELeverButtonType.Disable))
                    m_GOLeverActionToPerformOn.SetActive(false);
                else if (m_LeverButtonType.Equals(ELeverButtonType.PlatformMove))
                    m_GOLeverActionToPerformOn.GetComponent<WaypointMover>().enabled = true;
                foreach (ProCamera2DShake Cam in LevelManager.instance.m_ShakeCamList)
                    Cam.Shake(Cam.ShakePresets[0]);
            }
        }
    }
}

public enum ELeverButtonType
{
    Enable = 0,
    Disable = 1,
    PlatformMove = 2,
}

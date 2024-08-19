using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject m_MissionCompleteUI;
    [SerializeField] private GameObject m_MissionFailedUI;
    [SerializeField] private GameObject m_LevelUI;
    [SerializeField] private TextMeshProUGUI m_LevelText;


    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {

    }


    public static void ShowMissionCompleteUI()
    {
        instance.m_MissionCompleteUI.SetActive(true);
    }

    public static void HideMissionCompleteUI()
    {
        instance.m_MissionCompleteUI.SetActive(false);
    }

    public static void ShowMissionFailUI()
    {
        instance.m_MissionFailedUI.SetActive(true);
    }

    public static void HideMissionFailUI()
    {
        instance.m_MissionFailedUI.SetActive(false);
    }

    public static void ShowLevelUI()
    {
        instance.m_LevelText.text = SceneChange.GetCurrentLevel();
        instance.m_LevelUI.SetActive(true);
    }

    public static void HideLevelUI()
    {
        instance.m_LevelUI.SetActive(false);
    }
}

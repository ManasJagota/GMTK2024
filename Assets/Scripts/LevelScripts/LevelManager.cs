using Com.LuisPedroFonseca.ProCamera2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string m_MissionCompletedTag_Small;
    [SerializeField] string m_MissionCompletedTag_Large;

    [SerializeField] List<CharacterController> m_characterList;
    [SerializeField] AudioSource m_MissionSound;

    [SerializeField] AudioClip m_MissionCOmpleteclip;
    [SerializeField] AudioClip m_MissionFailedclip;
    [SerializeField] GameObject m_particleEffectOnVictory;
    [SerializeField] GameObject m_particleEffectOnLoss;

    public List<ProCamera2DShake> m_ShakeCamList;
    public static List<CharacterController> CharacterList {  get { return instance.m_characterList; } }

    public static string MissionCompletedTag_Small { get { return instance.m_MissionCompletedTag_Small; } set { instance.m_MissionCompletedTag_Small = value; } }
    public static string MissionCompletedTag_Large { get { return instance.m_MissionCompletedTag_Large; } set { instance.m_MissionCompletedTag_Large = value; } }

    public static LevelManager instance;
    private bool m_canSkipLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.ShowLevelUI();
        StartCoroutine(DelayToSkipLevel());
        PlayerPrefs.SetFloat(SceneChange.GetCurrentLevel() + "Unlocked", 1);
    }

    IEnumerator DelayToSkipLevel()
    {
        yield return new WaitForSeconds(3f);
        m_canSkipLevel = true;
    }

    private void OnDisable()
    {
        if(UIManager.instance != null)
            UIManager.HideLevelUI();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.HideMissionFailUI();
            SceneChange.ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.P) && m_canSkipLevel)
            SkipLevel();
    }

    public static void CheckIfMissionCompleted()
    {
        foreach(CharacterController character in CharacterList)
        {
            if (!character.m_isAccomplished)
                return;
        }
        Debug.Log("Mission Accomplished!");
        instance.MissionCOmpleted();
    }

    public static void MissionFailed(Vector3 position)
    {
        foreach (ProCamera2DShake Cam in instance.m_ShakeCamList)
            Cam.Shake(Cam.ShakePresets[0]);
        foreach (CharacterController character in CharacterList)
            character.gameObject.SetActive(false);
        Instantiate(instance.m_particleEffectOnLoss,position,Quaternion.identity);
        instance.m_MissionSound.Pause();
        instance.m_MissionSound.loop = false;
        instance.m_MissionSound.clip = instance.m_MissionFailedclip;
        instance.m_MissionSound.Play();
        instance.StartCoroutine(instance.SHowUI());
        
    }

    IEnumerator SHowUI()
    {
        yield return new WaitForSeconds(1f);
        UIManager.ShowMissionFailUI();
    }

    IEnumerator SHowUIVictory()
    {
        yield return new WaitForSeconds(1f);
        UIManager.ShowMissionCompleteUI();
    }

    private void MissionCOmpleted()
    {
        m_MissionSound.Pause();
        instance.m_MissionSound.loop = false;
        m_MissionSound.clip = m_MissionCOmpleteclip; 
        m_MissionSound.Play();
        foreach (CharacterController character in CharacterList)
        {
            character.gameObject.SetActive(false);
            Instantiate(instance.m_particleEffectOnVictory, character.transform.position, Quaternion.identity);
        }
        instance.StartCoroutine(instance.SHowUIVictory());
        instance.StartCoroutine(instance.WaitToLoadNextScene());
    }

    public void SkipLevel()
    {
       MissionCOmpleted();
    }

    IEnumerator WaitToLoadNextScene()
    {
        yield return new WaitForSeconds(3);
        UIManager.HideMissionCompleteUI();
        SceneChange.LoadNextScene();
    }
}

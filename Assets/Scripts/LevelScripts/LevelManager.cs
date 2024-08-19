using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string m_MissionCompletedTag_Small;
    [SerializeField] string m_MissionCompletedTag_Large;

    [SerializeField] List<CharacterController> m_characterList;
    [SerializeField] AudioSource m_MissionSound;

    [SerializeField] AudioClip m_MissionCOmpleteclip;
    [SerializeField] AudioClip m_MissionFailedclip;

    public static List<CharacterController> CharacterList {  get { return instance.m_characterList; } }

    public static string MissionCompletedTag_Small { get { return instance.m_MissionCompletedTag_Small; } set { instance.m_MissionCompletedTag_Small = value; } }
    public static string MissionCompletedTag_Large { get { return instance.m_MissionCompletedTag_Large; } set { instance.m_MissionCompletedTag_Large = value; } }

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.ShowLevelUI();
        PlayerPrefs.SetFloat(SceneChange.GetCurrentLevel() + "Unlocked", 1);
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
            SceneChange.ReloadScene();
            UIManager.HideMissionFailUI();
        }

        if (Input.GetKeyDown(KeyCode.P))
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

    public static void MissionFailed()
    {
        instance.m_MissionSound.Pause();
        instance.m_MissionSound.clip = instance.m_MissionFailedclip;
        instance.m_MissionSound.Play();
    }

    private void MissionCOmpleted()
    {
        m_MissionSound.Pause();
        m_MissionSound.clip = m_MissionCOmpleteclip; 
        m_MissionSound.Play();
        UIManager.ShowMissionCompleteUI();
        foreach (CharacterController character in CharacterList)
            character.gameObject.SetActive(false);
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

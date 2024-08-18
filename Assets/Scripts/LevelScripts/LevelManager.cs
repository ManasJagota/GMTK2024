using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string m_MissionCompletedTag_Small;
    [SerializeField] string m_MissionCompletedTag_Large;

    [SerializeField] List<CharacterController> m_characterList;

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
    }

    public static void CheckIfMissionCompleted()
    {
        foreach(CharacterController character in CharacterList)
        {
            if (!character.m_isAccomplished)
                return;
        }
        Debug.Log("Mission Accomplished!");
        UIManager.ShowMissionCompleteUI();
        Time.timeScale = 0;
        instance.StartCoroutine(instance.WaitToLoadNextScene());
    }

    IEnumerator WaitToLoadNextScene()
    {
        yield return new WaitForSeconds(3);
        UIManager.HideMissionCompleteUI();
        SceneChange.LoadNextScene();
    }
}

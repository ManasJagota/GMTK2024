using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    static SceneChange instance;
    private bool isSceneloading;

    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    
    public static string GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadParticularScene(string a_SceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(a_SceneName);
    }

    public static void ReloadScene()
    {
        if (!instance.isSceneloading)
        {
            instance.isSceneloading = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            instance.StartCoroutine( instance.WaitBeforeLoadingScreen());
        }
    }

   IEnumerator WaitBeforeLoadingScreen()
    {
        yield return new WaitForSeconds(1);
        isSceneloading = false;
    }
}

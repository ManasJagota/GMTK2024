using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_StartLevel : MonoBehaviour
{
    [SerializeField] string SceneName;

    private void OnEnable()
    {
        if (SceneName.Equals("Level 1"))
            return;
        else if (!PlayerPrefs.HasKey(SceneName + "Unlocked"))
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
            SceneChange.LoadParticularScene(SceneName);
    }
}

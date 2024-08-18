using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Levels : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_DisableObjects;
    [SerializeField]
    List<GameObject> m_EnableObjects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            foreach (GameObject obj in m_DisableObjects) 
                obj.SetActive(false);
            foreach (GameObject obj in m_EnableObjects) 
                obj.SetActive(true);
        }
    }
}

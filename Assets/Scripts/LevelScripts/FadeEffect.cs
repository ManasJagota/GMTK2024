using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    Image m_fadeImage;
    [SerializeField]
    float m_fadeTime;

    private void Start()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        // loop over 1 second backwards
        for (float i = m_fadeTime; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, i/m_fadeTime);
            yield return null;
        }
    }
}

using UnityEngine;

public class Play : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            SceneChange.LoadNextScene();
    }
}

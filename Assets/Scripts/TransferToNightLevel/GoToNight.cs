using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNight : MonoBehaviour
{
    public string nightSceneName = "night";  // Must match Build Settings name exactly

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered night trigger.");
            SceneManager.LoadScene(nightSceneName);
        }
    }
}

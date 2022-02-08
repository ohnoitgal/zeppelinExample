using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You're fine man");
                break;
            case "Finish":
                LevelCompletionSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }
    
    void LevelCompletionSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay); 
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}

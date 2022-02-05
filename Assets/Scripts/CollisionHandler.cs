using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You're fine man");
                break;
            case "Finish":
                Debug.Log("You finished the level man");
                break;
            case "Fuel":
                Debug.Log("You got fuel man");
                break;
            default:
                ReloadLevel();
                break;
        }

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
}

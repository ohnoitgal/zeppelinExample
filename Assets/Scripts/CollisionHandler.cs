using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successTune;
    [SerializeField] AudioClip failTune;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) {return;}
        

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
        isTransitioning = true;
        audioSource.Stop();
        successParticles.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(successTune);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        failParticles.Stop();
        failParticles.Play();
        audioSource.PlayOneShot(failTune);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay); 
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        isTransitioning = false;
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
        isTransitioning = false;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}

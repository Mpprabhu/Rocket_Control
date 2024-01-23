using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float LevelLoadDelay = 0.5f;
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }    
    }


    void StartCrashSequence()
    {
        // SFX upon success
        // Particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",LevelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartSuccessSequence()
    {
        // SFX upon success
        // Particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",LevelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings) //sceneCountInBuildSettings would count the total number of scenes in the build
        {
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

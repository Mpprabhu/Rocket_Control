using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float LevelLoadDelay = 0.5f;
    [SerializeField]AudioClip Crash, Success;
    [SerializeField]ParticleSystem CrashParticles, SuccessParticles;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondDebugKeys();    
    }

    void RespondDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }    

        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;    //Toggles from true to false or  vice versa
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable)     { return; }
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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Crash);
        CrashParticles.Play();
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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticles.Play();
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

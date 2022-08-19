using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 1f;

    AudioSource gameSoundSource;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    bool isTransitioning = false;

    void Start() {
        gameSoundSource = GetComponent<AudioSource>();
        
    }

    
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning ){return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        gameSoundSource.Stop();
        gameSoundSource.PlayOneShot(winSound);   
        // add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        gameSoundSource.Stop();
        gameSoundSource.PlayOneShot(crashSound);   
        // add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
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

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

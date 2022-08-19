using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 1f;
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello You hit friendly object");
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
        // add sound effect on win
        // add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // add sound effect on crash
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

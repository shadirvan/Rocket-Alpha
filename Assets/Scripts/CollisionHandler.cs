using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 1f;

    AudioSource gameSoundSource;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
        gameSoundSource = GetComponent<AudioSource>();
        
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
            collisionDisabled = !collisionDisabled; // Toggle collision variable on or off.
        }
        
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled ){return;}

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
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        gameSoundSource.Stop();
        gameSoundSource.PlayOneShot(crashSound);   
        crashParticle.Play();
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

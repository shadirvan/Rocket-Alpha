using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello You hit friendly object");
                break;
            case "Finish":
                Debug.Log("Congratulations on finishing the level");
                break;
            case "Fuel":
                Debug.Log("Fuel Refilled!");
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

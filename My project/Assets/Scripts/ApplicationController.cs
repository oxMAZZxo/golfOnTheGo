using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    public static ApplicationController Instance {get; private set;}
    public int CurrentLevel {get; private set;}
    void Awake()
    {
        if(Instance == null && Instance != this)
        {
            Instance = this;
            Application.targetFrameRate = 144;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Load a level scene by providing a scene index. All levels start from index 1; level one: 1, level two: 2 etc.......
    /// </summary>
    /// <param name="sceneIndex">The scene index.</param>
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        CurrentLevel = sceneIndex;
    }

    public void LoadNextLevel()
    {
        CurrentLevel +=1;
        SceneManager.LoadScene(CurrentLevel);
    }

}

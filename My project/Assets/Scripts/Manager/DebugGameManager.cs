using UnityEngine;

/// <summary>
/// Only to be used for prototyping systems in test scenes.
/// </summary>
public class DebugGameManager : MonoBehaviour
{
    public PlayerController testController;

    void Start()
    {
        if(testController == null)
        {
            Debug.LogError($"Test player controller has not been assigned");
        
        }    
        testController.EnableInput();
    }
}

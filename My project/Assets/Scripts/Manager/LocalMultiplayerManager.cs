using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMultiplayerManager : MonoBehaviour
{
    private PlayerCard[] players;

    void OnEnable()
    {
        PlayMenu.RequestGameStart += OnRequestGameStart;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0) { return; }

        // spawn players
        Debug.Log($"{scene.name} loaded");
    }

    void OnDisable()
    {
        PlayMenu.RequestGameStart -= OnRequestGameStart;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnRequestGameStart(object sender, PlayerCard[] joinedPlayers)
    {
        players = joinedPlayers;
        ApplicationController.Instance.LoadLevel(1);
    }


}
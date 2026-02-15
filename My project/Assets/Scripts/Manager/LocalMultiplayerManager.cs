using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMultiplayerManager : MonoBehaviour
{
    public static LocalMultiplayerManager Instance {get; private set;} 
    public static event EventHandler<PlayerController[]> PlayersSpawned;
    [SerializeField]private PlayerController playerPrefab;
    private PlayerData[] players;

    void Awake()
    {
        if(Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        PlayMenu.RequestGameStart += OnRequestGameStart;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0) { return; }

        PlayerController[] playerControllers = new PlayerController[players.Length];

        for(int i = 0; i < players.Length; i++)
        {
            PlayerController current = Instantiate(playerPrefab,new Vector3(0,0),Quaternion.identity);
            current.GetComponent<SpriteRenderer>().color = players[i].Color;
            // Assign player name to controller label.
            current.Data = players[i];
            playerControllers[i] = current;
        }

        PlayersSpawned?.Invoke(this, playerControllers);
    }

    void OnDisable()
    {
        PlayMenu.RequestGameStart -= OnRequestGameStart;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnRequestGameStart(object sender, PlayerData[] joinedPlayers)
    {
        players = joinedPlayers;
        ApplicationController.Instance.LoadLevel(1);
    }


}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMultiplayerManager : MonoBehaviour
{
    public static LocalMultiplayerManager Instance {get; private set;} 
    public static event EventHandler<PlayerData[]> PlayersSpawned;
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

        GameObject playerstart = GameObject.FindGameObjectWithTag("PlayerStart");
        if(playerstart == null)
        {
            Debug.LogError($"A 'PlayerStart' game object could not be found in {scene.name} scene");
            return;
        }

        SpawnPlayers(playerstart.transform.position);
    }

    private void SpawnPlayers(Vector3 spawnPosition)
    {
        for(int i = 0; i < players.Length; i++)
        {
            PlayerController current = Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
            current.GetComponent<SpriteRenderer>().color = players[i].Colour;
            players[i].Controller = current;
            players[i].Tries = 0;
        }

        PlayersSpawned?.Invoke(this, players);
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
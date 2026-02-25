using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMultiplayerManager : MonoBehaviour
{
    public static LocalMultiplayerManager Instance {get; private set;} 
    public static event Action<PlayerData[]> PlayersSpawned;
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
        Vector2 position = spawnPosition;
        for(int i = 0; i < players.Length; i++)
        {
            PlayerController current = Instantiate(playerPrefab,position,Quaternion.identity);
            SpriteRenderer renderer = current.GetComponent<SpriteRenderer>();
            renderer.color = players[i].Colour;
            renderer.sortingOrder = players.Length - i;
            players[i].Controller = current;
            players[i].Tries = 0;
            position.x += 1;
        }

        PlayersSpawned?.Invoke(players);
    }

    void OnDisable()
    {
        PlayMenu.RequestGameStart -= OnRequestGameStart;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnRequestGameStart(PlayerData[] joinedPlayers)
    {
        players = joinedPlayers;
        ApplicationController.Instance.LoadLevel(1);
    }


}
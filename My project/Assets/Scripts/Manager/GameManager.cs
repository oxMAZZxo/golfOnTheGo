using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerData[] activePlayers;
    private int currentPlayerIndex;
    public event Action<string, Color> UpdatePlayerTurn;

    void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        LocalMultiplayerManager.PlayersSpawned += OnPlayersSpawned;
        PlayerController.PlayerTried += OnPlayerTried;
        Pothole.PlayerPotted += OnPlayerPotted;
    }

    private void OnPlayerPotted(object sender, PlayerController e)
    {
        Pothole pothole = (Pothole)sender;
        e.gameObject.SetActive(false);
        e.DisableInput();
        foreach (PlayerData player in activePlayers)
        {
            if (player.Controller)
            {
                player.Score = pothole.Points - (player.Tries - 1);
                break;
            }
        }
        CheckLevelComplete();
    }

    private void CheckLevelComplete()
    {
        foreach (PlayerData player in activePlayers)
        {
            if (player.Controller.isActiveAndEnabled) { return; }
        }
        Debug.Log($"Level has been completed!");
        ApplicationController.Instance.LoadNextLevel();
    }

    private void OnPlayerTried(object sender, EventArgs eventArgs)
    {
        activePlayers[currentPlayerIndex].Controller.DisableInput();
        activePlayers[currentPlayerIndex].Tries++;
        bool valid = false;
        while (!valid)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % activePlayers.Length;
            if (activePlayers[currentPlayerIndex].Controller.isActiveAndEnabled) { valid = true; }
        }

        activePlayers[currentPlayerIndex].Controller.EnableInput();
        CameraFollow.Instance.Target = activePlayers[currentPlayerIndex].Controller.transform;
        UpdatePlayerTurn?.Invoke(activePlayers[currentPlayerIndex].Name,activePlayers[currentPlayerIndex].Colour);
    }


    private void OnPlayersSpawned(object sender, PlayerData[] e)
    {
        activePlayers = e;
        e[0].Controller.EnableInput();
        currentPlayerIndex = 0;
        UpdatePlayerTurn?.Invoke(e[0].Name,e[0].Colour);
        CameraFollow.Instance.Target = e[0].Controller.transform;
    }


    void OnDisable()
    {
        LocalMultiplayerManager.PlayersSpawned -= OnPlayersSpawned;
        PlayerController.PlayerTried -= OnPlayerTried;
    }
}

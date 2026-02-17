using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerData[] activePlayers;
    private int currentPlayerIndex;

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
                return;
            }
        }
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
    }


    private void OnPlayersSpawned(object sender, PlayerData[] e)
    {
        activePlayers = e;
        e[0].Controller.EnableInput();
        currentPlayerIndex = 0;
    }

    void OnDisable()
    {
        LocalMultiplayerManager.PlayersSpawned -= OnPlayersSpawned;
        PlayerController.PlayerTried -= OnPlayerTried;
    }
}

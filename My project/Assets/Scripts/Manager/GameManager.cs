using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerData[] activePlayers;
    private int currentPlayerIndex;
    public event Action<PlayerData> UpdatePlayerTurn;

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
        HandlePlayer(pothole,e);
        if (CheckLevelComplete())
        {
            ApplicationController.Instance.LoadNextLevel();
        }else
        {
            ActivateNextPlayer();
        }
    }

    private void HandlePlayer(Pothole pothole, PlayerController controller)
    {
        controller.DisableInput();
        controller.gameObject.SetActive(false);
        foreach (PlayerData player in activePlayers)
        {
            if (player.Controller == controller)
            {
                player.Score = pothole.Points - (player.Tries - 1);
                break;
            }
        }
    }

    private bool CheckLevelComplete()
    {
        foreach (PlayerData player in activePlayers)
        {
            if (player.Controller.isActiveAndEnabled) { return false; }
        }
        Debug.Log($"Level has been completed!");
        return true;
    }

    private void OnPlayerTried(object sender, EventArgs eventArgs)
    {
        activePlayers[currentPlayerIndex].Controller.DisableInput();
        activePlayers[currentPlayerIndex].Tries++;
        
        ActivateNextPlayer();
    }

    private void ActivateNextPlayer()
    {
        bool valid = false;
        while (!valid)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % activePlayers.Length;
            if (activePlayers[currentPlayerIndex].Controller.isActiveAndEnabled) { valid = true; }
        }

        activePlayers[currentPlayerIndex].Controller.EnableInput();
        CameraFollow.Instance.Target = activePlayers[currentPlayerIndex].Controller.transform;
        UpdatePlayerTurn?.Invoke(activePlayers[currentPlayerIndex]);
    }

    private void OnPlayersSpawned(object sender, PlayerData[] e)
    {
        activePlayers = e;
        e[0].Controller.EnableInput();
        currentPlayerIndex = 0;
        UpdatePlayerTurn?.Invoke(e[0]);
        CameraFollow.Instance.Target = e[0].Controller.transform;
    }


    void OnDisable()
    {
        LocalMultiplayerManager.PlayersSpawned -= OnPlayersSpawned;
        PlayerController.PlayerTried -= OnPlayerTried;
    }
}

using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private PlayerController[] activePlayers;
    private int currentPlayerIndex;

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
        LocalMultiplayerManager.PlayersSpawned += OnPlayersSpawned;
        PlayerController.PlayerTried += OnPlayerTried;
    }

    private void OnPlayerTried(object sender, int playerID)
    {
        activePlayers[currentPlayerIndex].DisableInput();
        Debug.Log($"Disabling {activePlayers[currentPlayerIndex].Data.Name} input");

        currentPlayerIndex = (currentPlayerIndex + 1) % activePlayers.Length;

        activePlayers[currentPlayerIndex].EnableInput();
        Debug.Log($"Enabling { activePlayers[currentPlayerIndex].Data.Name} input");

    }


    private void OnPlayersSpawned(object sender, PlayerController[] e)
    {
        activePlayers = e;
        Debug.Log($"Enabling {e[0].Data.Name} input");
        e[0].EnableInput();
        currentPlayerIndex = 0;
    }
    
    void OnDisable()
    {
        LocalMultiplayerManager.PlayersSpawned -= OnPlayersSpawned;
        PlayerController.PlayerTried -= OnPlayerTried;
    }
}

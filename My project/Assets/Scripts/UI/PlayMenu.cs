using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject addPlayerPanel;
    [SerializeField] private GameObject playerCardPrefab;
    [SerializeField] private GameObject listViewContainer;
    private List<PlayerData> joinedPlayers;
    private List<GameObject> joinedPlayersUI;
    public static event EventHandler<PlayerData[]> RequestGameStart; 

    void Start()
    {
        if (playerCardPrefab == null)
        {
            Debug.LogError($"Player Card Prefab has not been assigned, cannot add player card to list");
        }
        if (listViewContainer == null)
        {
            Debug.LogError($"List view container has not been assigned, cannot add player card to list");
        }
        if (addPlayerPanel == null)
        {
            Debug.LogError($"The AddPlayerPanel has not been assigned");
        }
        joinedPlayers = new List<PlayerData>();
        joinedPlayersUI = new List<GameObject>();
    }

    public void AddPlayer(string name)
    {
        float r; float g; float b;
        r = UnityEngine.Random.Range(0f,1f); 
        g = UnityEngine.Random.Range(0f, 1f);
        b = UnityEngine.Random.Range(0f, 1f);

        Color color = new Color(r,g,b);
        joinedPlayers.Add(new PlayerData(joinedPlayers.Count, name, color));
        GameObject card = Instantiate(playerCardPrefab, listViewContainer.transform);
        card.GetComponentInChildren<TMP_Text>().text = name;
        card.GetComponentInChildren<Image>().color = color;
        joinedPlayersUI.Add(card);
    }

    public void RequestStart()
    {
        if(joinedPlayers.Count < 1) {return;}
        RequestGameStart?.Invoke(this, joinedPlayers.ToArray());
    }

    void OnEnable()
    {
        addPlayerPanel.SetActive(false);
    }
}



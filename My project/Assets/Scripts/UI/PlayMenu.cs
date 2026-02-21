using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject addPlayerPanel;
    [SerializeField] private GameObject playerCardPrefab;
    [SerializeField] private GameObject listViewContainer;
    private ToggleGroup toggleGroup;
    private List<PlayerData> joinedPlayers;
    private List<Toggle> joinedPlayersUI;
    public static event Action<PlayerData[]> RequestGameStart; 

    void Start()
    {
        if (playerCardPrefab == null)
        {
            Debug.LogError($"Player Card Prefab has not been assigned, cannot add player card to list");
        }
        if(playerCardPrefab.GetComponent<Toggle>() == false)
        {
            Debug.LogError($"Player Card UI does not contain the 'Toggle' component.");
        }
        if (listViewContainer == null)
        {
            Debug.LogError($"List view container has not been assigned, cannot add player card to list");
        }
        if (addPlayerPanel == null)
        {
            Debug.LogError($"The AddPlayerPanel has not been assigned");
        }
        toggleGroup = listViewContainer.GetComponent<ToggleGroup>();
        joinedPlayers = new List<PlayerData>();
        joinedPlayersUI = new List<Toggle>();
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
        Toggle toggle = card.GetComponent<Toggle>();
        toggle.group = toggleGroup;

        joinedPlayersUI.Add(toggle);
    }

    public void RemovePlayer()
    {
        int index;
        for(index = 0; index < joinedPlayersUI.Count; index++)
        {
            if(joinedPlayersUI[index].isOn)
            {
                Debug.Log($"Removing {joinedPlayers[index].Name} at index {index}");
                joinedPlayers.RemoveAt(index);
                Destroy(joinedPlayersUI[index].gameObject);
                joinedPlayersUI.RemoveAt(index);
                break;
            }
        }
        
    }

    public void RequestStart()
    {
        if(joinedPlayers.Count < 1) {return;}
        RequestGameStart?.Invoke(joinedPlayers.ToArray());
    }

    void OnEnable()
    {
        addPlayerPanel.SetActive(false);
    }
}



using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    public static PlayMenu Instance { get; private set; }

    [SerializeField] private GameObject addPlayerPanel;
    [SerializeField] private GameObject playerCardPrefab;
    [SerializeField] private GameObject listViewContainer;
    private List<PlayerCard> joinedPlayers;
    public PlayerCard[] JoinedPlayers
    {
        get
        {
            return joinedPlayers.ToArray();
        }
    }
    private List<GameObject> joinedPlayersUI;

    void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        joinedPlayers = new List<PlayerCard>();
        joinedPlayersUI = new List<GameObject>();
    }

    public void AddPlayer(string name)
    {
        float r; float g; float b;
        r = Random.Range(0f,1f); 
        g = Random.Range(0f, 1f);
        b = Random.Range(0f, 1f);

        Color color = new Color(r,g,b);
        joinedPlayers.Add(new PlayerCard(joinedPlayers.Count, name, color));
        GameObject card = Instantiate(playerCardPrefab, listViewContainer.transform);
        card.GetComponentInChildren<TMP_Text>().text = name;
        card.GetComponentInChildren<Image>().color = color;
        joinedPlayersUI.Add(card);
    }

    void OnEnable()
    {
        addPlayerPanel.SetActive(false);
    }
}


public struct PlayerCard
{
    public int ID { get; }
    public string Name { get; }
    public Color Color {get;}

    public PlayerCard(int id, string name, Color color)
    {
        ID = id;
        Name = name;
        Color = color;
    }
}
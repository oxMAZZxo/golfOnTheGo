using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerListManager : MonoBehaviour
{
    // Reference to your PlayerCard template
    public VisualTreeAsset playerCardTemplate;

    private ListView listView;
    private Button addButton;

    // Data source for ListView
    private List<string> players = new List<string>();

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Grab ListView and Button from UXML
        listView = root.Q<ListView>("PlayerListView");
        addButton = root.Q<Button>("AddPlayerButton");

        // Setup ListView
        listView.itemsSource = players;

        // makeItem returns a cloned PlayerCard for each row
        listView.makeItem = () => playerCardTemplate.CloneTree();

        // bindItem sets the label text in the cloned card
        listView.bindItem = (element, index) =>
        {
            Label label = element.Q<Label>("PlayerNameLabel");
            label.text = players[index];
        };

        listView.selectionType = SelectionType.None;
        var scrollView = listView.Q<ScrollView>();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
        // Button adds a new player
        addButton.clicked += () =>
        {
            string newPlayerName = $"Player {players.Count}";
            players.Add(newPlayerName);
            listView.Rebuild(); // must call this for ListView to update and scroll
        };
    }
}

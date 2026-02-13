using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    private UIDocument ui;
    private Button playButton;

    void Start()
    {
        ui = GetComponent<UIDocument>();
        playButton = ui.rootVisualElement.Q<Button>("PlayButton");
        playButton.clicked += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        // do something
    }
}

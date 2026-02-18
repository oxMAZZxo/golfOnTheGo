using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    [SerializeField]private TMP_Text playerTurnDisplay;
    [SerializeField]private Image displayImage;

    void OnEnable()
    {
        GameManager.Instance.UpdatePlayerTurn += OnUpdatePlayerTurn;
    }

    private void OnUpdatePlayerTurn(string name, Color colour)
    {
        playerTurnDisplay.text = $"{name}'s Turn";
        displayImage.color = colour;
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    [SerializeField]private TMP_Text playerTurnLabel;
    [SerializeField]private TMP_Text playerScoreLabel;
    [SerializeField]private TMP_Text playerTriesLabel;
    [SerializeField]private Image displayImage;

    void OnEnable()
    {
        GameManager.Instance.UpdatePlayerTurn += OnUpdatePlayerTurn;
    }

    private void OnUpdatePlayerTurn(PlayerData playerData)
    {
        playerTurnLabel.text = $"{playerData.Name}'s Turn";
        playerScoreLabel.text = $"Score: {playerData.Score}";
        playerTriesLabel.text = $"Current Tries: {playerData.Tries}";

        displayImage.color = playerData.Colour;
    }

    void OnDisable()
    {
        GameManager.Instance.UpdatePlayerTurn -= OnUpdatePlayerTurn;
    }
}

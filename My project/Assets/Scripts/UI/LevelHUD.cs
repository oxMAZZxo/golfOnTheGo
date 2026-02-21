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
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted(PlayerData[] obj)
    {
        playerTurnLabel.gameObject.SetActive(false);
        playerScoreLabel.gameObject.SetActive(false);
        playerTriesLabel.gameObject.SetActive(false);
        displayImage.gameObject.SetActive(false);
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
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.UpdatePlayerTurn -= OnUpdatePlayerTurn;
    }
}
